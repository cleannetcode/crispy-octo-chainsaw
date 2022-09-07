using CrispyOctoChainsaw.API.Options;
using CSharpFunctionalExtensions;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CrispyOctoChainsaw.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class BaseController : ControllerBase
    {
        protected Result<Guid> UserId
        {
            get
            {
                var claim = HttpContext.User
                    .Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (claim is null)
                {
                    return Result.Failure<Guid>($"{nameof(claim)} cannot be null.");
                }

                var success = Guid.TryParse(claim.Value, out var userId);
                if (!success)
                {
                    return Result.Failure<Guid>($"{nameof(userId)} cannot parse.");
                }

                return userId;
            }
        }

        protected string CreateAccessToken(
            UserInformation information,
            JWTSecretOptions options)
        {
            var accsessToken = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret(options.Secret)
                      .ExpirationTime(DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim(ClaimTypes.Name, information.Nickname)
                      .AddClaim(ClaimTypes.NameIdentifier, information.UserId)
                      .AddClaim(ClaimTypes.Role, information.Role)
                      .WithVerifySignature(true)
                      .Encode();

            return accsessToken;
        }

        protected string CreateRefreshToken(
            UserInformation information,
            JWTSecretOptions options)
        {
            var refreshToken = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(options.Secret)
                    .ExpirationTime(DateTimeOffset.UtcNow.AddMonths(1).ToUnixTimeSeconds())
                    .AddClaim(ClaimTypes.Name, information.Nickname)
                    .AddClaim(ClaimTypes.NameIdentifier, information.UserId)
                    .AddClaim(ClaimTypes.Role, information.Role)
                    .WithVerifySignature(true)
                    .Encode();

            return refreshToken;
        }

        protected IDictionary<string, object> GetPayloadFromJWTToken(string token, JWTSecretOptions options)
        {
            var payload = JwtBuilder.Create()
                        .WithAlgorithm(new HMACSHA256Algorithm())
                        .WithSecret(options.Secret)
                        .MustVerifySignature()
                        .Decode<IDictionary<string, object>>(token);

            return payload;
        }

        protected Result<UserInformation> ParseUserInformation(IDictionary<string, object> payload)
        {
            Result failure = Result.Success();

            if (!payload.TryGetValue(ClaimTypes.NameIdentifier, out var nameIdentifierValue))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<UserInformation>("User id is not found."));
            }

            if (!Guid.TryParse((string)nameIdentifierValue, out var userId))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<UserInformation>($"{nameof(userId)} is can't parsing."));
            }

            if (!payload.TryGetValue(ClaimTypes.Role, out var roleValue))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<UserInformation>("Role is not found."));
            }

            var role = (string)roleValue;
            if (string.IsNullOrWhiteSpace(role))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<UserInformation>($"{nameof(role)} is can't parsing."));
            }

            if (!payload.TryGetValue(ClaimTypes.Name, out var nicknameValue))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<UserInformation>("Nickname is not found."));
            }

            var nickname = (string)nicknameValue;
            if (string.IsNullOrWhiteSpace(nickname))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<UserInformation>($"{nameof(nickname)} is can't parsing."));
            }

            if (failure.IsFailure)
            {
                return Result.Failure<UserInformation>(failure.Error);
            }

            return new UserInformation(nickname, userId, role);
        }
    }
}
