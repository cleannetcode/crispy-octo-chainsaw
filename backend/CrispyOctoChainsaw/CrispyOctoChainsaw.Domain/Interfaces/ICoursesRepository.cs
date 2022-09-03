using CrispyOctoChainsaw.Domain.Model;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ICoursesRepository
    {
        Task<Course[]> Get();

        Task<Course?> Get(int courseId);
    }
}
