using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface ISystemAdminService
    {
        Task<Result> Delete(string id);

        Task<User[]> Get();

        Task<Result<User>> Get(string id);
    }
}