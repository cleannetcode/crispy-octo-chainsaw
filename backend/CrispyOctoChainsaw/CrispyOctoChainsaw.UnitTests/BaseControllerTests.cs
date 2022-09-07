using CrispyOctoChainsaw.API;
using CrispyOctoChainsaw.API.Controllers;

namespace CrispyOctoChainsaw.UnitTests
{
    public class BaseControllerTests : BaseController
    {
        [Fact]
        public async Task ParseUserInformation_ShouldReturnUserInformaion()
        {
            // arrange
            var userId = Guid.NewGuid();
            var role = "User";
            var nickname = "UserNickname";

            var userInformation = new UserInformation(nickname, userId, role);

            var refreshToken = CreateRefreshToken(userInformation, new API.Options.JWTSecretOptions
            {
                Secret = "secret23423546464"
            });

            var payload = GetPayloadFromJWTToken(refreshToken, new API.Options.JWTSecretOptions
            {
                Secret = "secret23423546464"
            });

            // act
            var result = ParseUserInformation(payload);

            // assert
            Assert.Equal(userId, result.Value.UserId);
            Assert.Equal(nickname, result.Value.Nickname);
            Assert.Equal(role, result.Value.Role);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
        }
    }
}
