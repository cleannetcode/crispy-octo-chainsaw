namespace CrispyOctoChainsaw.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<bool> Delete(string id);

        Task<User[]> Get();

        Task<User?> Get(string id);
    }
}