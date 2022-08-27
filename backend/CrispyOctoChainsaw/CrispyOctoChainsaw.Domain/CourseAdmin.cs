using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain
{
    public record CourseAdmin
    {
        public const int MAX_LENGTH_NICKNAME = 50;

        private CourseAdmin(Guid courseAdminId, string nickname)
        {
            CourseAdminId = courseAdminId;
            Nickname = nickname;
        }

        public Guid CourseAdminId { get; }

        public string Nickname { get; }

        public static Result<CourseAdmin> Create(
            Guid courseAdminId,
            string nickname)
        {
            if (courseAdminId == Guid.Empty)
            {
                return Result.Failure<CourseAdmin>(
                    $"{nameof(courseAdminId)} is not be empty!");
            }

            if (string.IsNullOrWhiteSpace(nickname))
            {
                return Result.Failure<CourseAdmin>(
                    $"{nameof(nickname)} is not be null or whitespace");
            }

            var courseAdmin = new CourseAdmin(courseAdminId, nickname);

            return courseAdmin;
        }
    }
}
