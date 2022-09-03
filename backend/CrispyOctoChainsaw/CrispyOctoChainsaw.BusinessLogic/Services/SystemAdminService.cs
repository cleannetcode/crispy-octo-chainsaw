using CrispyOctoChainsaw.Domain.Interfaces;
using CrispyOctoChainsaw.Domain.Model;
using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.BusinessLogic.Services
{
    public class SystemAdminService : ISystemAdminService
    {
        private readonly IUsersRepository _usersRepository;

        public SystemAdminService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<User[]> Get()
        {
            return await _usersRepository.Get();
        }

        public async Task<Result<User>> Get(string id)
        {
            var user = await _usersRepository.Get(id);

            if (user is null)
            {
                return Result.Failure<User>("Нет пользователя с таким идентификатором");
            }

            return user;
        }

        public async Task<Result> Delete(string id)
        {
            var result = await _usersRepository.Delete(id);

            if (result == false)
            {
                return Result.Failure("Не удалось удалить пользователя с таким идентификатором");
            }

            return Result.Success();
        }
    }
}