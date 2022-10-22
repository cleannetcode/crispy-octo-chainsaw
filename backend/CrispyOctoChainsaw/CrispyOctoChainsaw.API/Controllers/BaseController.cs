using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

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
    }
}
