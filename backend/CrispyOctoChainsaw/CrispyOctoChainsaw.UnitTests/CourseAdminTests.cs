using CrispyOctoChainsaw.Domain.Model;
using CrispyOctoChainsaw.UnitTests.MemberData;

namespace CrispyOctoChainsaw.UnitTests
{
    public class CourseAdminTests
    {
        [Fact]
        public async Task CreateCourseAdmin_ShouldReturnNewCourseAdmin()
        {
            // arrange
            var courseAdminId = Guid.NewGuid();
            var nickName = "nickname";

            // act
            var course = CourseAdmin.Create(courseAdminId, nickName);

            // assert
            Assert.NotNull(course.Value);
            Assert.False(course.IsFailure);
        }

        [Theory]
        [MemberData(
            nameof(CourseAdminDataGenerator.GenerateSetInvalidGuidString),
            parameters: 10,
            MemberType = typeof(CourseAdminDataGenerator))]
        public async Task CreateCourseAdmin_CourseAdminIdAndNicknameIsNotValid_ShouldReturnErrors(
            Guid courseAdminId,
            string nickname)
        {
            // arrange
            // act
            var course = CourseAdmin.Create(courseAdminId, nickname);

            // assert
            Assert.False(course.IsSuccess);
            Assert.True(course.IsFailure);
        }
    }
}
