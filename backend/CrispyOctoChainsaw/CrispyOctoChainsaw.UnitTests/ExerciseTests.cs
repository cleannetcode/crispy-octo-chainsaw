using AutoFixture;
using CrispyOctoChainsaw.Domain.Model;
using CrispyOctoChainsaw.UnitTests.MemberData;

namespace CrispyOctoChainsaw.UnitTests
{
    public class ExerciseTests
    {
        private readonly Fixture _fixture;

        public ExerciseTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Create_ShouldReturnNewExercise()
        {
            // arrange
            var title = _fixture.Create<string>();
            var description = _fixture.Create<string>();
            var branchName = _fixture.Create<string>();
            var courseId = _fixture.Create<int>();

            // act
            var result = Exercise.Create(title, description, branchName, courseId);

            // assert
            Assert.NotNull(result.Value);
            Assert.False(result.IsFailure);
            Assert.Equal(title, result.Value.Title);
            Assert.Equal(description, result.Value.Description);
            Assert.Equal(branchName, result.Value.BranchName);
            Assert.Equal(courseId, result.Value.CourseId);
        }

        [Theory]
        [MemberData(
            nameof(ExerciseDataGenerator.GenerateSetInvalidAllParameters),
            parameters: 10,
            MemberType = typeof(ExerciseDataGenerator))]
        public void Create_AllParametersIsInvalid_ShouldReturnErrors(
            string title,
            string description,
            string branchName,
            int courseId)
        {
            // arrange
            // act
            var result = Exercise.Create(title, description, branchName, courseId);

            // assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.True(result.Error.Length != 0);
        }
    }
}
