using AutoFixture;
using CrispyOctoChainsaw.API.Contracts;
using CrispyOctoChainsaw.IntegrationalTests.MemberData;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace CrispyOctoChainsaw.IntegrationalTests.Tests
{
    public class CmsExercisesControllerTests : BaseControllerTest
    {
        public CmsExercisesControllerTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public async Task CreateExersice_ShouldReturnOk()
        {
            // arrange
            await CourseAdminLogin();
            var courseId = await MakeCourse();
            var request = Fixture.Build<CreateExerciseRequest>()
                .With(x => x.CourseId, courseId)
                .Create();

            // act
            var response = await Client.PostAsJsonAsync("api/cms/exercises", request);

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(
            nameof(CmsExercisesDataGenerator.GenerateSetInvalidTitleDescriptionBranchNameCourseId),
            parameters: 10,
            MemberType = typeof(CmsExercisesDataGenerator))]
        public async Task CreateExercise_InvalidTitleDescriptionBranchNameCourseId_ShouldReturnBadRequest(
            string title,
            string description,
            string branchName,
            int courseId)
        {
            // arrange
            await CourseAdminLogin();
            await MakeCourse();
            var request = Fixture.Build<CreateExerciseRequest>()
                .With(x => x.CourseId, courseId)
                .With(x => x.Title, title)
                .With(x => x.Description, description)
                .With(x => x.BranchName, branchName)
                .Create();

            // act
            var response = await Client.PostAsJsonAsync("api/cms/exercises", request);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task EditExercise_ShouldReturnOk()
        {
            // arrange
            await CourseAdminLogin();
            var courseId = await MakeCourse();
            var exerciseId = await MakeExercise(courseId);
            var request = Fixture.Build<EditExerciseRequest>()
                .With(x => x.CourseId, courseId)
                .Create();

            // act
            var response = await Client.PutAsJsonAsync($"api/cms/exercises/{exerciseId}", request);

            // assertS
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(
            nameof(CmsExercisesDataGenerator.GenerateSetInvalidTitleDescriptionBranchName),
            parameters: 10,
            MemberType = typeof(CmsExercisesDataGenerator))]
        public async Task EditExercise_InvalidTitleDescriptionBranchNameCourseId_ShouldReturnBadRequest(
            string title,
            string description,
            string branchName)
        {
            // arrange
            await CourseAdminLogin();
            var courseId = await MakeCourse();
            var exerciseId = await MakeExercise(courseId);
            var request = Fixture.Build<EditExerciseRequest>()
                .With(x => x.Title, title)
                .With(x => x.Description, description)
                .With(x => x.BranchName, branchName)
                .Create();

            // act
            var response = await Client.PutAsJsonAsync($"api/cms/exercises/{exerciseId}", request);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [MemberData(
            nameof(CmsExercisesDataGenerator.GenerateSetInvalidId),
            parameters: 10,
            MemberType = typeof(CmsExercisesDataGenerator))]
        public async Task EditExercise_InvalidCourseId_ShouldReturnBadRequest(int exerciseId)
        {
            // arrange
            await CourseAdminLogin();
            var validCourseId = await MakeCourse();
            await MakeExercise(validCourseId);
            var request = Fixture.Build<EditExerciseRequest>().Create();

            // act
            var response = await Client.PutAsJsonAsync($"api/cms/exercises/{exerciseId}", request);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteExercise_ShouldReturnOk()
        {
            // arrange
            await CourseAdminLogin();
            var courseId = await MakeCourse();
            var exerciseId = await MakeExercise(courseId);

            // act
            var response = await Client.DeleteAsync($"api/cms/exercises/{exerciseId}");

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(
            nameof(CmsExercisesDataGenerator.GenerateSetInvalidId),
            parameters: 10,
            MemberType = typeof(CmsExercisesDataGenerator))]
        public async Task DeleteExercise_InvalidExerciseId_ShouldReturnBadRequest(int exerciseId)
        {
            // arrange
            await CourseAdminLogin();
            var courseId = await MakeCourse();
            await MakeExercise(courseId);

            // act
            var response = await Client.DeleteAsync($"api/cms/exercises/{exerciseId}");

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
