using CrispyOctoChainsaw.Domain.Model;
using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ICmsCoursesRepository
    {
        Task<Result<Course[]>> GetAdminCourses(Guid courseAdminId);

        Task<Result<Course>> FindCourseById(int courseId);

        Task<Result<Course>> GetById(int courseId);

        Task<Result<int>> Create(Course newCourse, Guid curseAdminI);

        Task<Result<int>> Edit(Course editCourse);

        Task<Result<int>> Delete(int courseId);

        Task<Result<Exercise[]>> GetExercisesByCourseId(int courseId);
    }
}
