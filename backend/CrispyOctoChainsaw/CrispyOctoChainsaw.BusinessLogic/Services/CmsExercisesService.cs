using CrispyOctoChainsaw.Domain.Interfaces;
using CrispyOctoChainsaw.Domain.Model;
using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.BusinessLogic.Services
{
    public class CmsExercisesService : ICmsExercisesService
    {
        private readonly ICmsExercisesRepository _exercisesRepository;

        public CmsExercisesService(
            ICmsExercisesRepository exercisesRepository)
        {
            _exercisesRepository = exercisesRepository;
        }

        public async Task<Result<int>> Create(Exercise exercise)
        {
            var result = await _exercisesRepository.Create(exercise);
            if (result.IsFailure)
            {
                return Result.Failure<int>("Course not found.");
            }

            return result.Value;
        }

        public async Task<Result<int>> Edit(int id, Exercise editExercise)
        {
            var exercise = await _exercisesRepository.GetById(id);
            if (exercise.IsFailure)
            {
                return Result.Failure<int>("Course not found.");
            }

            var result = await _exercisesRepository.Edit(editExercise with { Id = exercise.Value.Id });

            return result.Value;
        }

        public async Task<Result<int>> Delete(int id)
        {
            var exercise = await _exercisesRepository.GetById(id);
            if (exercise.IsFailure)
            {
                return Result.Failure<int>("Course not found.");
            }

            var result = await _exercisesRepository.Delete(exercise.Value.Id);

            return result.Value;
        }
    }
}
