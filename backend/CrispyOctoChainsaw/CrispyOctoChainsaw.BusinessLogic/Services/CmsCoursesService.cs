using CrispyOctoChainsaw.Domain.Model;
using CrispyOctoChainsaw.Domain.Interfaces;
using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.BusinessLogic.Services
{
    public class CmsCoursesService : ICmsCoursesService
    {
        private readonly ICmsCoursesRepository _repository;

        public CmsCoursesService(ICmsCoursesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> CreateCourse(Course newCourse, Guid curseAdminId)
        {
            if (curseAdminId == Guid.Empty)
            {
                return Result.Failure<int>($"{nameof(curseAdminId)} is not valid.");
            }

            var courseId = await _repository.Create(newCourse);

            return courseId;
        }

        public async Task<Result<int>> EditCourse(int courseId, Course editCourse)
        {
            var course = await _repository.FindCourseById(courseId);
            if (course.IsFailure)
            {
                return Result.Failure<int>("Course not found.");
            }

            var editCourseId = await _repository.Edit(
                editCourse with { Id = course.Value.Id });

            return editCourseId.Value;
        }

        public async Task<Result<Course[]>> GetAdminCourses(Guid courseAdminId)
        {
            var courses = await _repository.GetAdminCourses(courseAdminId);
            if (courses.IsFailure)
            {
                return Result.Failure<Course[]>(
                    $"Course with {nameof(courseAdminId)} not found.");
            }

            return courses.Value;
        }

        public async Task<Result<int>> Delete(int courseId)
        {
            var course = await _repository.FindCourseById(courseId);
            if (course.IsFailure)
            {
                return Result.Failure<int>("Course not found.");
            }

            var result = await _repository.Delete(course.Value.Id);

            return result.Value;
        }
    }
}
