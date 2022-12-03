using CrispyOctoChainsaw.Domain.Model;
using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ICmsCoursesService
    {
        Task<Result<Course[]>> GetAdminCourses(Guid courseAdminId);

        Task<Result<int>> CreateCourse(Course newCourse, Guid curseAdminId);

        Task<Result<int>> EditCourse(int courseId, Course editCourse);

        Task<Result<int>> Delete(int courseId);

        Task<Result<Exercise[]>> GetExercisesByCourseId(int courseId);

        Task<Result<Course>> GetById(int courseId);
    }
}
