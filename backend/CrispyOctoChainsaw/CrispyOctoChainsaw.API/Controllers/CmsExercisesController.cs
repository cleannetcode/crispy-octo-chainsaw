using AutoMapper;
using CrispyOctoChainsaw.API.Contracts;
using CrispyOctoChainsaw.Domain.Interfaces;
using CrispyOctoChainsaw.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrispyOctoChainsaw.API.Controllers
{
    [Authorize(Roles = nameof(Roles.CourseAdmin))]
    [Route("api/cms/exercises")]
    public class CmsExercisesController : BaseController
    {
        private readonly ILogger<CmsExercisesController> _logger;
        private readonly ICmsExercisesService _service;
        private readonly IMapper _mapper;

        public CmsExercisesController(
            ILogger<CmsExercisesController> logger,
            ICmsExercisesService service,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Create exercise.
        /// </summary>
        /// <param name="request">Create exercise information.</param>
        /// <returns>Exercise id.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateExersice([FromBody] CreateExerciseRequest request)
        {
            var result = Exercise.Create(
                request.Title,
                request.Description,
                request.BranchName,
                request.CourseId);

            if (result.IsFailure)
            {
                _logger.LogError("{errors}", result.Error);
                return BadRequest(result.Error);
            }

            var exercise = await _service.Create(result.Value);
            if (exercise.IsFailure)
            {
                _logger.LogError("{errors}", exercise.Error);
                return BadRequest(exercise.Error);
            }

            return Ok(exercise.Value);
        }

        /// <summary>
        /// Edit exercise.
        /// </summary>
        /// <param name="exerciseId">Id.</param>
        /// <param name="request">Edit exercise information.</param>
        /// <returns>Exercise id.</returns>
        [HttpPut("{exerciseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditExercise([FromRoute] int exerciseId, [FromBody] EditExerciseRequest request)
        {
            var result = Exercise.Create(
                request.Title,
                request.Description,
                request.BranchName,
                request.CourseId);

            if (result.IsFailure)
            {
                _logger.LogError("{errors}", result.Error);
                return BadRequest(result.Error);
            }

            var exercise = await _service.Edit(exerciseId, result.Value);
            if (exercise.IsFailure)
            {
                _logger.LogError("{errors}", exercise.Error);
                return BadRequest(exercise.Error);
            }

            return Ok(exercise.Value);
        }

        /// <summary>
        /// Delete exercise.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Exercise id.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteExercise([FromRoute] int id)
        {
            var result = await _service.Delete(id);
            if (result.IsFailure)
            {
                _logger.LogError("{errors}", result.Error);
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
