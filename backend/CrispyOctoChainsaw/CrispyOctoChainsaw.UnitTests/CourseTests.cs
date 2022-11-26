using AutoFixture;
using CrispyOctoChainsaw.Domain.Model;
using CrispyOctoChainsaw.UnitTests.MemberData;

namespace CrispyOctoChainsaw.UnitTests
{
    public class CourseTests
    {
        private readonly Fixture _fixture;

        public CourseTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Create_ShouldReturnNewCourse()
        {
            // arrange
            var title = _fixture.Create<string>();
            var description = _fixture.Create<string>();
            var repositoryName = _fixture.Create<string>();
            var courseAdminId = _fixture.Create<Guid>();
            var bannerName = _fixture.Create<string>();

            // act
            var result = Course.Create(title, description, repositoryName, bannerName, courseAdminId);

            // assert
            Assert.NotNull(result.Value);
            Assert.False(result.IsFailure);
            Assert.Equal(title, result.Value.Title);
            Assert.Equal(description, result.Value.Description);
            Assert.Equal(repositoryName, result.Value.RepositoryName);
            Assert.Equal(courseAdminId, result.Value.CourseAdminId);
            Assert.Equal(bannerName, result.Value.BannerName);
        }

        [Theory]
        [MemberData(
            nameof(CourseDataGenerator.GenerateSetInvalidAllParameters),
            parameters: 10,
            MemberType = typeof(CourseDataGenerator))]
        public void Create_AllParametersIsInvalid_ShouldReturnErrors(
            string title,
            string description,
            string repositoryName,
            Guid courseAdminId,
            string bannerName)
        {
            // arrange
            // act
            var result = Course.Create(title, description, repositoryName, bannerName, courseAdminId);

            // assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.True(result.Error.Length != 0);
        }
    }
}
