using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CrispyOctoChainsaw.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class BaseController : ControllerBase
    {
    }
}
