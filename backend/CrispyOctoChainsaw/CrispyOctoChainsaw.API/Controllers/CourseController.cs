using CrispyOctoChainsaw.API.Contracts;
using CrispyOctoChainsaw.Domain.Model;
using CrispyOctoChainsaw.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrispyOctoChainsaw.API.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ILogger<CourseController> _logger;
        private readonly ICourseAdminService _service;

        public CourseController(
            ILogger<CourseController> logger,
            ICourseAdminService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Get admin courses.
        /// </summary>
        /// <returns>Courses.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCourseResponse[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAdminCourses()
        {
            var courseAdminId = Guid.NewGuid();

            var result = await _service.GetAdminCourses(courseAdminId);

            if (result.IsFailure)
            {
                _logger.LogError("{errors}", result.Error);
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Create course.
        /// </summary>
        /// <param name="createRequest">Course information.</param>
        /// <returns>Course Id.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCourse(
            [FromBody] CreateCourseRequest createRequest)
        {
            var courseAdminId = Guid.NewGuid();
            var newCourse = Course.Create(createRequest.Title, createRequest.Description, createRequest.RepositoryName);
            if (newCourse.IsFailure)
            {
                _logger.LogError("{errors}", newCourse.Error);
                return BadRequest(newCourse.Error);
            }

            var result = await _service.CreateCourse(newCourse.Value, courseAdminId);
            if (result.IsFailure)
            {
                _logger.LogError("{errors}", result.Error);
                return BadRequest(result.Error);
            }

            return Ok(result);
        }

        /// <summary>
        /// Edit course.
        /// </summary>
        /// <param name="courseId">Course id.</param>
        /// <param name="editRequest">Edit course information.</param>
        /// <returns>Course Id.</returns>
        [HttpPut("{courseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditCourse(
            [FromRoute] int courseId,
            [FromBody] EditCourseRequest editRequest)
        {
            var editCourse = Course.Create(editRequest.Title, editRequest.Description, editRequest.RepositoryName);
            if (editCourse.IsFailure)
            {
                _logger.LogError("{errors}", editCourse.Error);
                return BadRequest(editCourse.Error);
            }

            var result = await _service.EditCourse(courseId, editCourse.Value);
            if (result.IsFailure)
            {
                _logger.LogError("{errors}", result.Error);
                return BadRequest(result.Error);
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete course.
        /// </summary>
        /// <param name="courseId">Course id.</param>
        /// <returns>Course Id.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{courseId:int}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
        {
            var result = await _service.Delete(courseId);
            if (result.IsFailure)
            {
                _logger.LogError("{errors}", result.Error);
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
