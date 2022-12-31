using CrispyOctoChainsaw.API;
using CrispyOctoChainsaw.API.Contracts;
using CrispyOctoChainsaw.IntegrationalTests.MemberData;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace CrispyOctoChainsaw.IntegrationalTests.Tests
{
    public class UsersAccountControllerTests : BaseControllerTest
    {
        public UsersAccountControllerTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public async Task RegistrationUser_ShouldReturnOk()
        {
            // arrange
            var registrationRequest = new CourseAdminRegistrationRequest
            {
                Email = Email,
                Password = Password,
                Nickname = UserNickname
            };

            // act
            var response = await Client.PostAsJsonAsync("api/usersaccount/user/registration", registrationRequest);

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(
            nameof(UsersAccountDataGenerator.GenerateSetInvalidEmail),
            parameters: 10,
            MemberType = typeof(UsersAccountDataGenerator))]
        public async Task RegistrationUser_InvalidEmail_ShouldReturnBadRequest(
            string email)
        {
            var registrationRequest = new CourseAdminRegistrationRequest
            {
                Email = email,
                Password = Password,
                Nickname = UserNickname
            };

            // act
            var response = await Client.PostAsJsonAsync("api/usersaccount/user/registration", registrationRequest);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [MemberData(
            nameof(UsersAccountDataGenerator.GenerateSetInvalidPassword),
            parameters: 10,
            MemberType = typeof(UsersAccountDataGenerator))]
        public async Task RegistrationUser_InvalidPassword_ShouldReturnBadRequest(
            string password)
        {
            var registrationRequest = new CourseAdminRegistrationRequest
            {
                Email = Email,
                Password = password,
                Nickname = UserNickname
            };

            // act
            var response = await Client.PostAsJsonAsync("api/usersaccount/user/registration", registrationRequest);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task RegistrationUser_UserAlreadyExist_ShouldReturnBadRequest()
        {
            // arrange
            var registrationRequest = new CourseAdminRegistrationRequest
            {
                Email = Email,
                Password = Password,
                Nickname = UserNickname
            };

            // act
            await Client.PostAsJsonAsync("api/usersaccount/user/registration", registrationRequest);
            var response = await Client.PostAsJsonAsync("api/usersaccount/user/registration", registrationRequest);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_ShouldReturnOk()
        {
            // arrange
            var registrationRequest = new CourseAdminRegistrationRequest
            {
                Password = Password,
                Email = Email,
                Nickname = UserNickname
            };

            var loginRequest = new LoginRequest
            {
                Email = Email,
                Password = Password
            };

            // act
            await Client.PostAsJsonAsync("api/usersaccount/user/registration", registrationRequest);
            var response = await Client.PostAsJsonAsync("api/usersaccount/login", loginRequest);

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Login_SessionIsAlreadyExist_ShouldReturnOk()
        {
            // arrange
            var registrationRequest = new CourseAdminRegistrationRequest
            {
                Password = Password,
                Email = Email,
                Nickname = UserNickname
            };

            var loginRequest = new LoginRequest
            {
                Email = Email,
                Password = Password
            };

            // act
            await Client.PostAsJsonAsync("api/usersaccount/user/registration", registrationRequest);
            await Client.PostAsJsonAsync("api/usersaccount/login", loginRequest);
            var response = await Client.PostAsJsonAsync("api/usersaccount/login", loginRequest);

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequest()
        {
            // arrange
            var registrationRequest = new CourseAdminRegistrationRequest
            {
                Password = Password,
                Email = Email,
                Nickname = UserNickname
            };

            var loginRequest = new LoginRequest
            {
                Email = "11111@gmail.com",
                Password = "12345"
            };

            // act
            await Client.PostAsJsonAsync("api/usersaccount/user/registration", registrationRequest);
            var response = await Client.PostAsJsonAsync("api/usersaccount/login", loginRequest);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task RefreshToken_ShouldReturnOk()
        {
            // arrange
            var (accessToken, refreshToken) = await MakeSession();

            var refreshRequest = new TokenRequest
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            // act
            var response = await Client.PostAsJsonAsync("api/usersaccount/refreshaccesstoken", refreshRequest);

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Refreshing_access_token_when_refresh_tokens_not_equals()
        {
            // arrange
            var (accessToken, refreshToken) = await MakeSession();
            var userInformation = new UserInformation("NewUser", UserId, "User");
            var anotherRefreshToken = CreateRefreshToken(userInformation);

            var refreshRequest = new TokenRequest
            {
                AccessToken = accessToken,
                RefreshToken = anotherRefreshToken
            };

            // act
            var response = await Client.PostAsJsonAsync("api/usersaccount/refreshaccesstoken", refreshRequest);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Refreshing_access_token_when__user_session_not_found()
        {
            // arrange
            var (accessToken, refreshToken) = await MakeSession();
            var userInformation = new UserInformation("NewUser", Guid.NewGuid(), "User");
            var anotherRefreshToken = CreateRefreshToken(userInformation);

            var refreshRequest = new TokenRequest
            {
                AccessToken = accessToken,
                RefreshToken = anotherRefreshToken
            };

            // act
            var response = await Client.PostAsJsonAsync("api/usersaccount/refreshaccesstoken", refreshRequest);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
