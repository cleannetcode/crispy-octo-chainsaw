using System.ComponentModel.DataAnnotations;

namespace CrispyOctoChainsaw.API.Contracts
{
    // Contract for login
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
