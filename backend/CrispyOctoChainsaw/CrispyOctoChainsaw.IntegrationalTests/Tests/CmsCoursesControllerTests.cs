using AutoFixture;
using CrispyOctoChainsaw.API.Contracts;
using Xunit.Abstractions;
using System.Net;
using CrispyOctoChainsaw.IntegrationalTests.MemberData;
using System.Net.Http.Headers;

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
            await MakeCourse();

            // act
            var response = await Client.GetAsync("api/cms/courses");

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetExercises_ShouldReturnOk()
        {
            // arrange
            await CourseAdminLogin();
            var courseId = await MakeCourse();
            await MakeExercise(courseId);

            // act
            var response = await Client.GetAsync($"api/cms/courses/{courseId}/exercises");

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(
            nameof(CmsCoursesDataGenerator.GenerateSetInvalidCourseId),
            parameters: 10,
            MemberType = typeof(CmsCoursesDataGenerator))]
        public async Task GetExercises_InvalidCourseId_ShouldReturnBadRequest(int courseId)
        {
            // arrange
            await CourseAdminLogin();
            var validCourseId = await MakeCourse();
            await MakeExercise(validCourseId);

            // act
            var response = await Client.GetAsync($"api/cms/courses/{courseId}/exercises");

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturnOk()
        {
            // arrange
            await CourseAdminLogin();
            var course = Fixture.Build<CreateCourseRequest>()
                .Without(x => x.ImageFile)
                .Create();

            var file = await MakeImage();
            using var httpContent = new MultipartFormDataContent("sdsvsv");
            using var fileContent = new ByteArrayContent(file);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpg");

            httpContent.Add(new StringContent(course.Title), "Title");
            httpContent.Add(new StringContent(course.Description), "Description");
            httpContent.Add(new StringContent(course.RepositoryName), "RepositoryName");
            httpContent.Add(fileContent, "image", "TestBanner.jpg");

            // act
            var response = await Client.PostAsync("api/cms/courses", httpContent);

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
            string repositoryName,
            byte[] file)
        {
            // arrange
            await CourseAdminLogin();
            var course = Fixture.Build<CreateCourseRequest>()
                .With(x => x.Title, title)
                .With(x => x.Description, description)
                .With(x => x.RepositoryName, repositoryName)
                .Without(x => x.ImageFile)
                .Create();

            using var httpContent = new MultipartFormDataContent("sdsvsv");
            using var fileContent = new ByteArrayContent(file);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpg");

            httpContent.Add(new StringContent(course.Title), "Title");
            httpContent.Add(new StringContent(course.Description), "Description");
            httpContent.Add(new StringContent(course.RepositoryName), "RepositoryName");
            httpContent.Add(fileContent, "image", "TestBanner.jpg");

            // act
            var response = await Client.PostAsync("api/cms/courses", httpContent);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Edit_ShouldReurnOk()
        {
            // arrange
            await CourseAdminLogin();
            var courseId = await MakeCourse();
            var editCourse = Fixture.Build<EditCourseRequest>()
                .Without(x => x.ImageFile)
                .Create();

            var file = await MakeImage();
            using var httpContent = new MultipartFormDataContent("sdsvsv");
            using var fileContent = new ByteArrayContent(file);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpg");

            httpContent.Add(new StringContent(editCourse.Title), "Title");
            httpContent.Add(new StringContent(editCourse.Description), "Description");
            httpContent.Add(new StringContent(editCourse.RepositoryName), "RepositoryName");
            httpContent.Add(fileContent, "image", "TestBanner.jpg");

            // act
            var response = await Client.PutAsync($"api/cms/courses/{courseId}", httpContent);

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
            string repositoryName,
            byte[] file)
        {
            // arrange
            await CourseAdminLogin();
            var courseId = await MakeCourse();

            var editCourse = Fixture.Build<EditCourseRequest>()
                .With(x => x.Title, title)
                .With(x => x.Description, description)
                .With(x => x.RepositoryName, repositoryName)
                .Without(x => x.ImageFile)
                .Create();

            using var httpContent = new MultipartFormDataContent("sdsvsv");
            using var fileContent = new ByteArrayContent(file);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpg");

            httpContent.Add(new StringContent(editCourse.Title), "Title");
            httpContent.Add(new StringContent(editCourse.Description), "Description");
            httpContent.Add(new StringContent(editCourse.RepositoryName), "RepositoryName");
            httpContent.Add(fileContent, "image", "TestBanner.jpg");

            // act
            var response = await Client.PutAsync($"api/cms/courses/{courseId}", httpContent);

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