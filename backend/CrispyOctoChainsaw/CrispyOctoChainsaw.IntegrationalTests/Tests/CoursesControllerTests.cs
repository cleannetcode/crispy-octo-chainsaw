using CrispyOctoChainsaw.IntegrationalTests.MemberData;
using System.Net;
using Xunit.Abstractions;

namespace CrispyOctoChainsaw.IntegrationalTests.Tests
{
    public class CoursesControllerTests : BaseControllerTest
    {
        public CoursesControllerTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public async Task Get_ShouldReturnOk()
        {
            // arrange
            await MakeCourse();

            // act
            var response = await Client.GetAsync("api/courses");

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetById_ShouldReturnOk()
        {
            // arrange
            await MakeCourse();
            var courseId = await MakeCourse();

            // act
            var response = await Client.GetAsync($"api/courses/{courseId}");

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(
            nameof(CoursesControllerDataGenerator.GenerateSetInvalidId),
            parameters: 5,
            MemberType = typeof(CoursesControllerDataGenerator))]
        public async Task GetById_InvalidCourseId_ShouldReturnBadRequest(int courseId)
        {
            // arrange
            await MakeCourse();
            await MakeCourse();

            // act
            var response = await Client.GetAsync($"api/courses/{courseId}");

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
