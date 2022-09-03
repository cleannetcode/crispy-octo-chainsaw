using CrispyOctoChainsaw.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrispyOctoChainsaw.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseRepository courseRepository, ILogger<CoursesController> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var courses = await _courseRepository.Get();
            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> Get(long courseId)
        {
            var course = await _courseRepository.Get(courseId);
            if (course is null)
            {
                return NotFound($"Course with id : {courseId} not found");
            }
            return Ok(course);
        }
    }
}
