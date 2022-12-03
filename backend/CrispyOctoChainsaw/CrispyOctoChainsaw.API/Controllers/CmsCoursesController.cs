using CrispyOctoChainsaw.API.Contracts;
using CrispyOctoChainsaw.Domain.Model;
using CrispyOctoChainsaw.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using CrispyOctoChainsaw.API.Options;
using AutoMapper;

namespace CrispyOctoChainsaw.API.Controllers
{
    [Authorize(Roles = nameof(Roles.CourseAdmin))]
    [Route("api/cms/courses")]
    public class CmsCoursesController : BaseController
    {
        private readonly ILogger<CmsCoursesController> _logger;
        private readonly ICmsCoursesService _service;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly FileSettings _fileSettings;

        public CmsCoursesController(
            ILogger<CmsCoursesController> logger,
            ICmsCoursesService service,
            IWebHostEnvironment env,
            IOptionsMonitor<FileSettings> optionMonitor,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _env = env;
            _mapper = mapper;
            _fileSettings = optionMonitor.CurrentValue;
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
            var courseAdminId = UserId.Value;

            var result = await _service.GetAdminCourses(courseAdminId);
            if (result.IsFailure)
            {
                _logger.LogError("{errors}", result.Error);
                return BadRequest(result.Error);
            }

            var groupsContract = _mapper.Map<Course[], GetCourseResponse[]>(result.Value);

            return Ok(groupsContract);
        }

        /// <summary>
        /// Get course by id.
        /// </summary>
        /// <param name="courseId">Course id.</param>
        /// <returns>Course.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCourseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{courseId:int}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int courseId)
        {
            var userId = UserId.Value;

            var course = await _service.GetById(userId, courseId);
            if (course.IsFailure)
            {
                _logger.LogError("{errors}", course.Error);
                return BadRequest(course.Error);
            }

            return Ok(course.Value);
        }

        /// <summary>
        /// Get exercise from course by id.
        /// </summary>
        /// <param name="courseId">Course id.</param>
        /// <returns>Exercises.</returns>
        [HttpGet("{courseId:int}/exercises")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExerciseResponse[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetExercises([FromRoute] int courseId)
        {
            var result = await _service.GetExercisesByCourseId(courseId);
            if (result.IsFailure)
            {
                _logger.LogError("{errors}", result.Error);
                return BadRequest(result.Error);
            }

            var exercisesEntity = _mapper.Map<Exercise[], GetExerciseResponse[]>(result.Value);

            return Ok(exercisesEntity);
        }

        /// <summary>
        /// Create course.
        /// </summary>
        /// <param name="createRequest">Course information.</param>
        /// <returns>Course Id.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateCourse(
            [FromForm] CreateCourseRequest createRequest)
        {
            var courseAdminId = UserId.Value;
            if (createRequest.ImageFile.Length == 0)
            {
                _logger.LogError("{errors}", "ImageFile is empty.");
                return BadRequest("ImageFile is empty.");
            }

            var bannerName = await SaveImage(createRequest.ImageFile);

            var newCourse = Course.Create(
                createRequest.Title,
                createRequest.Description,
                createRequest.RepositoryName,
                bannerName,
                courseAdminId);

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

            return Ok(result.Value);
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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditCourse(
            [FromRoute] int courseId,
            [FromForm] EditCourseRequest editRequest)
        {
            var courseAdminId = UserId.Value;
            if (editRequest.ImageFile is null)
            {
                _logger.LogError("{errors}", "ImageFile is empty.");
                return BadRequest("ImageFile is empty.");
            }

            var bannerName = await SaveImage(editRequest.ImageFile);

            var editCourse = Course.Create(
                editRequest.Title,
                editRequest.Description,
                editRequest.RepositoryName,
                bannerName,
                courseAdminId);

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

            return Ok(result.Value);
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

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            var directoryPath = Path.Combine(_env.ContentRootPath, $"{_fileSettings.Path}");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_env.ContentRootPath, $"{_fileSettings.Path}", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }
    }
}
