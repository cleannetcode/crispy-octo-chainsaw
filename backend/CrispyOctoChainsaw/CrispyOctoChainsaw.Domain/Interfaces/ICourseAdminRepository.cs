using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ICourseAdminRepository
    {
        Task<Result<Course[]>> GetAdminCourses(Guid courseAdminId);

        Task<Result<Course>> FindCourseById(int courseId);

        Task<Result<int>> Create(Course newCourse);

        Task<Result<int>> Edit(Course editCourse);

        Task<Result<int>> Delete(int courseId);
    }
}
