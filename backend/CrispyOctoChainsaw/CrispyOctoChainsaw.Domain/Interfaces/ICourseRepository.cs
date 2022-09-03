using CrispyOctoChainsaw.Domain.Model;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course[]> Get();
        Task<Course?> Get(long courseId);
    }
}
