using AutoMapper;
using CrispyOctoChainsaw.DataAccess.Postgres.Entities;
using CrispyOctoChainsaw.Domain.Interfaces;
using CrispyOctoChainsaw.Domain.Model;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace CrispyOctoChainsaw.DataAccess.Postgres.Repositories
{
    public class CmsExercisesRepository : ICmsExercisesRepository
    {
        private readonly CrispyOctoChainsawDbContext _context;
        private readonly IMapper _mapper;

        public CmsExercisesRepository(CrispyOctoChainsawDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<int>> Create(Exercise exercise)
        {
            var exerciseEntity = _mapper.Map<Exercise, ExerciseEntity>(exercise);
            await _context.Exercises.AddAsync(exerciseEntity);
            await _context.SaveChangesAsync();

            return exerciseEntity.Id;
        }

        public async Task<Result<int>> Delete(int id)
        {
            var exerciseEntity = await _context.Exercises.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (exerciseEntity is null)
            {
                return Result.Failure<int>("Exercise not found.");
            }

            return exerciseEntity.Id;
        }

        public async Task<Result<int>> Edit(Exercise exercise)
        {
            var exerciseEntity = _mapper.Map<Exercise, ExerciseEntity>(exercise);
            _context.Update(exerciseEntity);
            await _context.SaveChangesAsync();

            return exerciseEntity.Id;
        }

        public async Task<Result<Exercise>> GetById(int id)
        {
            var exercise = await _context.Exercises.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (exercise is null)
            {
                return Result.Failure<Exercise>("Exercise not found.");
            }

            return _mapper.Map<ExerciseEntity, Exercise>(exercise);
        }
    }
}
