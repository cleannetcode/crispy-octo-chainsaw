using CrispyOctoChainsaw.Domain.Interfaces;
using CrispyOctoChainsaw.Domain.Model;
using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.BusinessLogic.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly ICoursesRepository _courseRepository;

        public CoursesService(ICoursesRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Course[]> Get()
        {
            return await _courseRepository.Get();
        }

        public async Task<Result<Course>> Get(int courseId)
        {
            var course = await _courseRepository.Get(courseId);

            if (course == null)
            {
                return Result.Failure<Course?>($"Course with id : {courseId} not found");
            }

            return course;
        }
    }
}
