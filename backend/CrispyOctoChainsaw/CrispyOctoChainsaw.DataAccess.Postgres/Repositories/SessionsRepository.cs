using AutoMapper;
using CrispyOctoChainsaw.DataAccess.Postgres.Entities;
using CrispyOctoChainsaw.Domain.Interfaces;
using CrispyOctoChainsaw.Domain.Model;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace CrispyOctoChainsaw.DataAccess.Postgres.Repositories
{
    public class SessionsRepository : ISessionsRepository
    {
        private readonly CrispyOctoChainsawDbContext _context;
        private readonly IMapper _mapper;

        public SessionsRepository(CrispyOctoChainsawDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Delete(Guid userId)
        {
            var session = await GetById(userId);
            if (session.IsFailure)
            {
                return Result.Failure<bool>(session.Error);
            }

            _context.Remove(new SessionEntity { UserId = session.Value.UserId });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<bool>> Create(Session session)
        {
            if (session is null)
            {
                return Result.Failure<bool>("Session can't be null");
            }

            var getSession = await GetById(session.UserId);
            if (!getSession.IsFailure)
            {
                await Edit(session);
                return true;
            }

            var sessionEntity = _mapper.Map<Session, SessionEntity>(session);
            await _context.Sessions.AddAsync(sessionEntity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<Session>> GetById(Guid userId)
        {
            var session = await _context.Sessions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (session is null)
            {
                return Result.Failure<Session>($"Session with don't found.");
            }

            return _mapper.Map<SessionEntity, Session>(session);
        }

        private async Task<Result<bool>> Edit(Session session)
        {
            if (session is null)
            {
                return Result.Failure<bool>($"Session with id don't found.");
            }

            var sessionEntity = _mapper.Map<Session, SessionEntity>(session);
            _context.Sessions.Update(sessionEntity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
