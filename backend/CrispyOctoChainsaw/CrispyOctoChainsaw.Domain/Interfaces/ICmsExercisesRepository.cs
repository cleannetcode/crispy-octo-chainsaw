using CrispyOctoChainsaw.Domain.Model;
using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ICmsExercisesRepository
    {
        Task<Result<int>> Create(Exercise exercise);

        Task<Result<int>> Edit(Exercise exercise);

        Task<Result<Exercise>> GetById(int id);

        Task<Result<int>> Delete(int id);
    }
}
