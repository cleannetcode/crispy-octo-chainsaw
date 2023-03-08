using System.Net.Http.Headers;
using System.Security.Claims;
using AutoFixture;
using CrispyOctoChainsaw.API;
using CrispyOctoChainsaw.API.Contracts;
using CrispyOctoChainsaw.API.Options;
using CrispyOctoChainsaw.DataAccess.Postgres;
using CrispyOctoChainsaw.DataAccess.Postgres.Entities;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Xunit.Abstractions;

namespace CrispyOctoChainsaw.IntegrationalTests.Tests
{
    [Collection("Database collection")]
    public abstract class BaseControllerTest : IAsyncLifetime
    {
        private Guid _id = Guid.Parse("d4ddeb36-c32c-46fd-8aa1-f0a31d9a6a9b");
        private static readonly string _baseDirectory = AppContext.BaseDirectory;
        private static readonly string _path = Directory.GetParent(_baseDirectory).FullName;

        public BaseControllerTest(ITestOutputHelper outputHelper)
        {
            var app = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(service =>
                    {
                        service.Configure<FileSettings>(settings =>
                        {
                            settings.DirectoryName = "TestImages";
                        });
                    });

                    builder.ConfigureAppConfiguration((context, configurationBuilder) =>
                    {
                        var configuration = configurationBuilder
                            .SetBasePath(_path)
                            .AddJsonFile("testsettings.json")
                            .AddUserSecrets(typeof(BaseControllerTest).Assembly)
                            .Build();

                        CourseAdminId = configuration.GetValue<Guid>("Secrets:CourseAdminId");
                        if (CourseAdminId == null || CourseAdminId == Guid.Empty)
                        {
                            throw new ArgumentException($"{nameof(CourseAdminId)} - is required. Please setup testsettings");
                        }

                        UserId = configuration.GetValue<Guid>("Secrets:UserId");
                        if (UserId == null || UserId == Guid.Empty)
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
            var env = app.Services.CreateScope().ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            CleaningPath = Path.Combine(env.ContentRootPath, "TestImages");
        }

        public string ConnectionString { get; set; }

        protected HttpClient Client { get; set; }

        protected string CleaningPath { get; set; }

        protected Guid CourseAdminId { get; set; }

        protected Guid UserId { get; set; }

        protected string JwtTokenSecret { get; set; }

        protected Fixture Fixture { get; set; }

        protected string CourseAdminNickname => "CourseAdmin";

        protected string UserNickname => "User";

        protected string CourseAdminRole => "CourseAdmin";

        protected string UserRole => "User";

        protected string Email = "testEmail@gmail.com";

        protected string Password => "Qwaszx!@12";

        protected CrispyOctoChainsawDbContext DbContext { get; set; }

        protected async Task CourseAdminLogin()
        {
            var userInformation = new UserInformation(CourseAdminNickname, CourseAdminId, CourseAdminRole);
            var token = CreateAccessToken(userInformation);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                token);
        }

        protected async Task UserLogin()
        {
            var userIdInformation = new UserInformation(UserNickname, UserId, UserRole);
            var token = CreateAccessToken(userIdInformation);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                token);
        }

        protected async Task<int> MakeCourse()
        {
            var course = Fixture.Build<CourseEntity>()
                .Without(x => x.Id)
                .With(x => x.CourseAdminId, CourseAdminId)
                .Without(x => x.Exercises)
                .Without(x => x.DeleteTime)
                .Create();

            await DbContext.Courses.AddAsync(course);
            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            return course.Id;
        }

        protected async Task<int> MakeExercise(int courseId)
        {
            var exercise = Fixture.Build<ExerciseEntity>()
                .Without(x => x.Id)
                .Without(x => x.Course)
                .With(x => x.CourseId, courseId)
                .Create();

            await DbContext.AddAsync(exercise);
            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            return exercise.Id;
        }

        protected async Task<(string AccessToken, string RefreshToken)> MakeSession(Guid g = default(Guid))
        {
            var userInformation = new UserInformation(UserNickname, UserId, "User");

            var accessToken = CreateAccessToken(userInformation);
            var refreshToken = CreateRefreshToken(userInformation);

            var session = new SessionEntity
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserId = UserId
            };

            await DbContext.Sessions.AddAsync(session);
            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            return (accessToken, refreshToken);
        }

        protected async Task<byte[]> MakeImage()
        {
            var fileBytes = File.ReadAllBytes(@"TestImages/TestBanner.jpg");
            return fileBytes;
        }

        protected async Task<MultipartFormDataContent> MakeFormData(CreateCourseRequest request, byte[] image)
        {
            var httpContent = new MultipartFormDataContent("sdsvsv");
            var fileContent = new ByteArrayContent(image);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpg");

            httpContent.Add(new StringContent(request.Title), "Title");
            httpContent.Add(new StringContent(request.Description), "Description");
            httpContent.Add(new StringContent(request.RepositoryName), "RepositoryName");
            httpContent.Add(fileContent, "image", "TestBanner.jpg");

            return httpContent;
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

            var directoryInfo = new DirectoryInfo(CleaningPath);
            foreach (var file in directoryInfo.GetFiles())
            {
                file.Delete();
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