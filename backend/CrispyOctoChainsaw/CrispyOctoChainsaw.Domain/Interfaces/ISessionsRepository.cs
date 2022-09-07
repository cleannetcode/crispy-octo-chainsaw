using CrispyOctoChainsaw.Domain.Model;
using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ISessionsRepository
    {
        Task<Result<Session>> GetById(Guid userId);

        Task<Result<bool>> Create(Session session);

        Task<Result<bool>> Delete(Guid userId);
    }
}
