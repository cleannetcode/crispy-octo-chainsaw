namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<bool> Delete(Guid id);

        Task<User[]> Get();

        Task<User?> Get(Guid id);
    }
}