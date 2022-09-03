using CrispyOctoChainsaw.Domain.Model;
using CrispyOctoChainsaw.Domain.Interfaces;
using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.BusinessLogic.Services
{
    public class SystemAdminsService : ISystemAdminsService
    {
        private readonly IUsersRepository _usersRepository;

        public SystemAdminsService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<User[]> Get()
        {
            return await _usersRepository.Get();
        }

        public async Task<Result<User>> Get(Guid id)
        {
            var user = await _usersRepository.Get(id);

            if (user is null)
            {
                return Result.Failure<User>("Нет пользователя с таким идентификатором");
            }

            return user;
        }

        public async Task<Result> Delete(Guid id)
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