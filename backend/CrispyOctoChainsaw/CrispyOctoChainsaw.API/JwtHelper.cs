using CrispyOctoChainsaw.API.Options;
using CSharpFunctionalExtensions;
using JWT.Algorithms;
using JWT.Builder;
using System.Security.Claims;

namespace CrispyOctoChainsaw.API;

public class JwtHelper
{
    public static string CreateAccessToken(
        UserInformation information,
        JWTSecretOptions options)
    {
        var accessToken = JwtBuilder.Create()
                  .WithAlgorithm(new HMACSHA256Algorithm())
                  .WithSecret(options.Secret)
                  .ExpirationTime(DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                  .AddClaim(ClaimTypes.Name, information.Nickname)
                  .AddClaim(ClaimTypes.NameIdentifier, information.UserId)
                  .AddClaim(ClaimTypes.Role, information.Role)
                  .WithVerifySignature(true)
                  .Encode();

        return accessToken;
    }

    public static string CreateRefreshToken(
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

    public static Result<UserInformation> GetPayloadFromJWTTokenV2(
        string token,
        JWTSecretOptions options)
    {
        var payload = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(options.Secret)
                    .MustVerifySignature()
                    .Decode<UserInformation>(token);

        return payload;
    }

    public static IDictionary<string, object> GetPayloadFromJWTToken(
        string token,
        JWTSecretOptions options)
    {
        var payload = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(options.Secret)
                    .MustVerifySignature()
                    .Decode<IDictionary<string, object>>(token);

        return payload;
    }

    public static Result<UserInformation> ParseUserInformation(
        IDictionary<string, object> payload)
    {
        Result failure = Result.Success();

        if (!payload.TryGetValue(ClaimTypes.NameIdentifier, out var nameIdentifierValue) && string.IsNullOrWhiteSpace((string)nameIdentifierValue))
        {
            failure = Result.Combine(
                failure,
                Result.Failure<UserInformation>("User id is not found."));
        }

        if (!Guid.TryParse((string)nameIdentifierValue, out var userId))
        {
            failure = Result.Combine(
                failure,
                Result.Failure<UserInformation>(
                    $"{nameof(userId)} is can't parsing."));
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
                Result.Failure<UserInformation>(
                    $"{nameof(role)} is can't parsing."));
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
                Result.Failure<UserInformation>(
                    $"{nameof(nickname)} is can't parsing."));
        }

        if (failure.IsFailure)
        {
            return Result.Failure<UserInformation>(failure.Error);
        }

        return new UserInformation(nickname, userId, role);
    }
}
