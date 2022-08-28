namespace CrispyOctoChainsaw.API.Contracts
{
    public class GetUserResponse
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;
    }
}
