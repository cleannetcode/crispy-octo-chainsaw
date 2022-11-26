using CrispyOctoChainsaw.Domain.Model;
using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ICmsExercisesService
    {
        Task<Result<int>> Create(Exercise exercise);

        Task<Result<int>> Edit(int id, Exercise exercise);

        Task<Result<int>> Delete(int id);
    }
}
