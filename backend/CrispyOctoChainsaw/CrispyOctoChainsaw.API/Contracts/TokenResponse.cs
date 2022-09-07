namespace CrispyOctoChainsaw.API.Contracts
{
    // Contract for response AccessToken and RefreshToken and Nickname.
    public class TokenResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string Nickname { get; set; }
    }
}
