using CrispyOctoChainsaw.API;

namespace CrispyOctoChainsaw.UnitTests
{
    public class JwtHelperTests
    {
        [Fact]
        public async Task CreateAccessToken_ShouldReturnAccessToken()
        {
            // arrange
            var userId = Guid.NewGuid();
            var role = "User";
            var nickname = "UserNickname";

            var userInformation = new UserInformation(nickname, userId, role);

            // act
            var accessToken = JwtHelper.CreateAccessToken(userInformation, new API.Options.JWTSecretOptions
            {
                Secret = "secret23423546464"
            });

            // assert
            var payload = JwtHelper.GetPayloadFromJWTToken(accessToken, new API.Options.JWTSecretOptions
            {
                Secret = "secret23423546464"
            });

            var result = JwtHelper.ParseUserInformation(payload);

            Assert.False(string.IsNullOrWhiteSpace(accessToken));
            Assert.Equal(userId, result.Value.UserId);
            Assert.Equal(nickname, result.Value.Nickname);
            Assert.Equal(role, result.Value.Role);
        }

        [Fact]
        public async Task CreateRefreshToken_ShouldReturnRefreshToken()
        {
            // arrange
            var userId = Guid.NewGuid();
            var role = "User";
            var nickname = "UserNickname";

            var userInformation = new UserInformation(nickname, userId, role);

            // act
            var refreshToken = JwtHelper.CreateAccessToken(userInformation, new API.Options.JWTSecretOptions
            {
                Secret = "secret23423546464"
            });

            // assert
            var payload = JwtHelper.GetPayloadFromJWTToken(refreshToken, new API.Options.JWTSecretOptions
            {
                Secret = "secret23423546464"
            });

            var result = JwtHelper.ParseUserInformation(payload);

            Assert.False(string.IsNullOrWhiteSpace(refreshToken));
            Assert.Equal(userId, result.Value.UserId);
            Assert.Equal(nickname, result.Value.Nickname);
            Assert.Equal(role, result.Value.Role);
        }

        [Fact]
        public async Task ParseUserInformation_ShouldReturnUserInformaion()
        {
            // arrange
            var userId = Guid.NewGuid();
            var role = "User";
            var nickname = "UserNickname";

            var userInformation = new UserInformation(nickname, userId, role);

            var refreshToken = JwtHelper.CreateRefreshToken(userInformation, new API.Options.JWTSecretOptions
            {
                Secret = "secret23423546464"
            });

            var payload = JwtHelper.GetPayloadFromJWTToken(refreshToken, new API.Options.JWTSecretOptions
            {
                Secret = "secret23423546464"
            });

            // act
            var result = JwtHelper.ParseUserInformation(payload);

            // assert
            Assert.Equal(userId, result.Value.UserId);
            Assert.Equal(nickname, result.Value.Nickname);
            Assert.Equal(role, result.Value.Role);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
        }
    }
}
