using AutoMapper;
using CrispyOctoChainsaw.API.Contracts;
using CrispyOctoChainsaw.Domain.Interfaces;
using CrispyOctoChainsaw.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace CrispyOctoChainsaw.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService _coursesService;
        private readonly ILogger<CoursesController> _logger;
        private readonly IMapper _mapper;


        public CoursesController(ICoursesService courseService, ILogger<CoursesController> logger, IMapper mapper)
        {
            _coursesService = courseService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var courses = await _coursesService.Get();

            var response = _mapper.Map<Course[], GetCourseResponse[]>(courses);

            return Ok(response);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> Get(int courseId)
        {
            var course = await _coursesService.Get(courseId);
            
            if (course.IsFailure)
            {
                _logger.LogError("{error}", course.Error);
                return BadRequest(course.Error);
            }

            var response = _mapper.Map<Course, GetCourseResponse>(course.Value);

            return Ok(response);
        }
    }
}
