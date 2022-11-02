using System.Security.Claims;
using Newtonsoft.Json;

namespace CrispyOctoChainsaw.API
{
    public record UserInformation
    {
        public UserInformation(string nickname, Guid userId, string role)
        {
            Nickname = nickname;
            UserId = userId;
            Role = role;
        }

        [JsonProperty(ClaimTypes.Name)]
        public string Nickname { get; init; }

        [JsonProperty(ClaimTypes.NameIdentifier)]
        public Guid UserId { get; init; }

        [JsonProperty(ClaimTypes.Role)]
        public string Role { get; init; }
    }
}
