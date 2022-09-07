using System.ComponentModel.DataAnnotations;
using CrispyOctoChainsaw.Domain;

namespace CrispyOctoChainsaw.API.Contracts
{
    // Contract for registration courseAdmin
    public class CourseAdminRegistrationRequest
    {

        [MaxLength(CourseAdmin.MaxLengthNickname)]
        public string Nickname { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
