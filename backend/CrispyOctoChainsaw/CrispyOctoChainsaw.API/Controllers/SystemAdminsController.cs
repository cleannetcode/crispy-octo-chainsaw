using AutoMapper;
using CrispyOctoChainsaw.API.Contracts;
using CrispyOctoChainsaw.Domain;
using CrispyOctoChainsaw.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrispyOctoChainsaw.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAdminsController : ControllerBase
    {
        private readonly ISystemAdminService _systemAdminService;
        private readonly IMapper _mapper;
        private readonly ILogger<SystemAdminsController> _logger;

        public SystemAdminsController(
            ISystemAdminService systemAdminService,
            IMapper mapper,
            ILogger<SystemAdminsController> logger)
        {
            _systemAdminService = systemAdminService;
            _mapper = mapper;   
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _systemAdminService.Get();

            var response = _mapper.Map<User[], GetUserResponse[]>(users);

            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _systemAdminService.Get(id);

            if (user.IsFailure)
            {
                _logger.LogError("{error}", user.Error);
                return BadRequest(user.Error);
            }

            var response = _mapper.Map<User, GetUserResponse>(user.Value);

            return Ok(response);
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _systemAdminService.Delete(id);

            if (response.IsFailure)
            {
                _logger.LogError("{error}", response.Error);
                return BadRequest(response.Error);
            }

            return Ok(response.IsSuccess);
        }
    }
}
