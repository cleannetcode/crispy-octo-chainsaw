using CrispyOctoChainsaw.Domain.Model;
using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ICoursesService
    {
        Task<Course[]> Get();

        Task<Result<Course>> Get(long courseId);
    }
}
