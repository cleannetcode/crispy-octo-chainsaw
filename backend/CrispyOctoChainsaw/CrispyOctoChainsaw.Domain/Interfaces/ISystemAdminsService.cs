using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ISystemAdminsService
    {
        Task<Result> Delete(Guid id);

        Task<User[]> Get();

        Task<Result<User>> Get(Guid id);
    }
}