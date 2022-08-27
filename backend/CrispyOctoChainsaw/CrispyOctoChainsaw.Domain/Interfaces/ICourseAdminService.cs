using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ICourseAdminService
    {
        Task<Result<Course[]>> GetAdminCourses(Guid courseAdminId);

        Task<Result<int>> CreateCourse(Course newCourse, Guid curseAdminId);

        Task<Result<int>> EditCourse(int courseId, Course editCourse);

        Task<Result<int>> Delete(int courseId);
    }
}
