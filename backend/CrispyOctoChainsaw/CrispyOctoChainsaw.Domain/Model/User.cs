using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Model
{
    public record User
    {
        public string Id { get; }

        public string UserName { get; }

        private User(string id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public static Result<User> Create(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Result.Failure<User>("Имя пользователя не может быть пустым");
            }

            return new User(string.Empty, userName);
        }
    }
}
