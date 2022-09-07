using AutoFixture;
using CrispyOctoChainsaw.API.Contracts;
using Xunit.Abstractions;
using System.Net.Http.Json;
using System.Net;
using CrispyOctoChainsaw.IntegrationalTests.MemberData;

namespace CrispyOctoChainsaw.IntegrationalTests.Tests
{
    public class CmsCoursesControllerTests : BaseControllerTest
    {
        public CmsCoursesControllerTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public async Task Get_ShouldRetunrOk()
        {
            // arrange
            await CourseAdminLogin();

            // act
            var response = await Client.GetAsync("api/cms/courses");

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_ShouldReturnOk()
        {
            // arrange
            await CourseAdminLogin();
            var course = Fixture.Create<CreateCourseRequest>();

            // act
            var response = await Client.PostAsJsonAsync("api/cms/courses", course);

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(
            nameof(CmsCoursesDataGenerator.GenerateSetInvalidTitleDescriptionRepositotyName),
            parameters: 10,
            MemberType = typeof(CmsCoursesDataGenerator))]
        public async Task Create_ShouldReturnBadRequest(
            string title,
            string description,
            string repositoryName)
        {
            // arrange
            await CourseAdminLogin();
            var course = Fixture.Build<CreateCourseRequest>()
                .With(x => x.Title, title)
                .With(x => x.Description, description)
                .With(x => x.RepositoryName, repositoryName)
                .Create();

            // act
            var response = await Client.PostAsJsonAsync("api/cms/courses", course);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Edit_ShouldReurnOk()
        {
            // arrange
            await CourseAdminLogin();
            var courseId = await MakeCourse();
            var editCourse = Fixture.Create<EditCourseRequest>();

            // act
            var response = await Client.PutAsJsonAsync($"api/cms/courses/{courseId}", editCourse);

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(
            nameof(CmsCoursesDataGenerator.GenerateSetInvalidTitleDescriptionRepositotyName),
            parameters: 10,
            MemberType = typeof(CmsCoursesDataGenerator))]
        public async Task Edit_ShouldReturnBadRequest(
            string title,
            string description,
            string repositoryName)
        {
            // arrange
            await CourseAdminLogin();
            var courseId = await MakeCourse();
            var editCourse = Fixture.Build<EditCourseRequest>()
                .With(x => x.Title, title)
                .With(x => x.Description, description)
                .With(x => x.RepositoryName, repositoryName)
                .Create();

            // act
            var response = await Client.PutAsJsonAsync($"api/cms/courses/{courseId}", editCourse);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk()
        {
            // arrange
            await CourseAdminLogin();
            var courseId = await MakeCourse();

            // act
            var response = await Client.DeleteAsync($"api/cms/courses/{courseId}");

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(
            nameof(CmsCoursesDataGenerator.GenerateSetInvalidCourseId),
            parameters: 10,
            MemberType = typeof(CmsCoursesDataGenerator))]
        public async Task Delete_ShouldReturnBadRequest(int courseId)
        {
            // arrange
            await CourseAdminLogin();

            // act
            var response = await Client.DeleteAsync($"api/cms/courses/{courseId}");

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}