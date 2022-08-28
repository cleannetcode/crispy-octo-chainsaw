using AutoMapper;
using CrispyOctoChainsaw.API.Contracts;
using CrispyOctoChainsaw.Domain;
using CrispyOctoChainsaw.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrispyOctoChainsaw.API.Controllers
{
    public class SystemAdminsController : BaseController
    {
        private readonly ISystemAdminsService _systemAdminService;
        private readonly IMapper _mapper;
        private readonly ILogger<SystemAdminsController> _logger;

        public SystemAdminsController(
            ISystemAdminsService systemAdminService,
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

        [HttpGet("{userId:Guid}")]
        public async Task<IActionResult> Get(Guid id)
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

        [HttpDelete("{userId:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
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
