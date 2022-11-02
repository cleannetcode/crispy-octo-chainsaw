using AutoFixture;
using CrispyOctoChainsaw.API;
using CrispyOctoChainsaw.DataAccess.Postgres;
using CrispyOctoChainsaw.DataAccess.Postgres.Entities;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using System.Net.Http.Headers;
using System.Security.Claims;
using Xunit.Abstractions;

namespace CrispyOctoChainsaw.IntegrationalTests.Tests
{
    [Collection("Database collection")]
    public abstract class BaseControllerTest : IAsyncLifetime
    {
        private static readonly string _baseDirectory = AppContext.BaseDirectory;

        private static readonly string _path = Directory.GetParent(_baseDirectory).FullName;

        public BaseControllerTest(ITestOutputHelper outputHelper)
        {
            var app = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureAppConfiguration((context, configurationBuilder) =>
                    {
                        var configuration = configurationBuilder
                            .SetBasePath(_path)
                            .AddJsonFile("testsettings.json")
                            .AddUserSecrets(typeof(BaseControllerTest).Assembly)
                            .Build();

                        CourseAdminId = configuration
                           .GetSection("Secrets:CourseAdminId")
                           .Value;

                        if (string.IsNullOrWhiteSpace(CourseAdminId))
                        {
                            throw new ArgumentException($"{nameof(CourseAdminId)} - is required. Please setup testsettings");
                        }

                        UserId = configuration
                            .GetSection("Secrets:UserId")
                            .Value;

                        if (string.IsNullOrWhiteSpace(UserId))
                        {
                            throw new ArgumentException($"{nameof(UserId)} - is required");
                        }

                        JwtTokenSecret = configuration
                            .GetSection("JWTSecret:Secret")
                            .Value;

                        if (string.IsNullOrWhiteSpace(JwtTokenSecret))
                        {
                            throw new ArgumentException($"{nameof(JwtTokenSecret)} - is required");
                        }

                        ConnectionString = configuration.GetConnectionString(nameof(CrispyOctoChainsawDbContext));
                    });
                });

            Client = app.CreateDefaultClient(new LoggingHandler(outputHelper));
            Fixture = new Fixture();
            DbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<CrispyOctoChainsawDbContext>();
        }

        protected HttpClient Client { get; set; }

        protected string CourseAdminId { get; set; }

        protected string UserId { get; set; }

        protected string JwtTokenSecret { get; set; }

        protected Fixture Fixture { get; set; }

        protected string CourseAdminNickname => "CourseAdmin";

        protected string UserNickname => "User";

        protected string CourseAdminRole => "CourseAdmin";

        protected string UserRole => "User";

        protected string Email = "testEmail@gmail.com";

        protected string Password => "Qwaszx!@12";

        protected CrispyOctoChainsawDbContext DbContext { get; set; }

        private async Task<Guid> GetGuidId(string id)
        {
            Guid.TryParse(id, out var parseId);

            return parseId;
        }

        protected async Task CourseAdminLogin()
        {
            var courseAdminId = await GetGuidId(CourseAdminId);

            var userInformation = new UserInformation(CourseAdminNickname, courseAdminId, CourseAdminRole);
            var token = CreateAccessToken(userInformation);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                token);
        }

        protected async Task UserLogin()
        {
            var userId = await GetGuidId(UserId);

            var userIdInformation = new UserInformation(UserNickname, userId, UserRole);
            var token = CreateAccessToken(userIdInformation);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                token);
        }

        protected async Task<int> MakeCourse()
        {
            var courseAdminId = await GetGuidId(CourseAdminId);

            var course = Fixture.Build<CourseEntity>()
                .Without(x => x.Id)
                .With(x => x.CourseAdminId, courseAdminId)
                .Create();

            await DbContext.Courses.AddAsync(course);
            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            return course.Id;
        }

        protected async Task<(string AccessToken, string RefreshToken)> MakeSession()
        {
            var userId = await GetGuidId(UserId);

            var userInformation = new UserInformation(UserNickname, userId, "User");

            var accessToken = CreateAccessToken(userInformation);
            var refreshToken = CreateRefreshToken(userInformation);

            var session = new SessionEntity
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserId = userId
            };

            await DbContext.Sessions.AddAsync(session);
            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            return (accessToken, refreshToken);
        }

        protected string CreateAccessToken(UserInformation information)
        {
            var accsessToken = JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(JwtTokenSecret)
                .ExpirationTime(DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                .AddClaim(ClaimTypes.Name, information.Nickname)
                .AddClaim(ClaimTypes.NameIdentifier, information.UserId)
                .AddClaim(ClaimTypes.Role, information.Role)
                .WithVerifySignature(true)
                .Encode();

            return accsessToken;
        }

        protected string CreateRefreshToken(UserInformation information)
        {
            var refreshToken = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(JwtTokenSecret)
                    .ExpirationTime(DateTimeOffset.UtcNow.AddMonths(1).ToUnixTimeSeconds())
                    .AddClaim(ClaimTypes.Name, information.Nickname)
                    .AddClaim(ClaimTypes.NameIdentifier, information.UserId)
                    .AddClaim(ClaimTypes.Role, information.Role)
                    .WithVerifySignature(true)
                    .Encode();

            return refreshToken;
        }

        public string ConnectionString { get; set; }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            var respawner = await CreateRespawner();

            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();
                await respawner.ResetAsync(conn);
            }
        }

        private async Task<Respawner> CreateRespawner()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync();

            var respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres
            });

            return respawner;
        }
    }
}

