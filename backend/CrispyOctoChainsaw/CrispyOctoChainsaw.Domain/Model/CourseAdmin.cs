using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Model
{
    public record CourseAdmin
    {
        public const int MaxLengthNickname = 50;

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
            Result failure = Result.Success();
            if (courseAdminId == Guid.Empty)
            {
                failure = Result.Failure<CourseAdmin>(
                    $"{nameof(courseAdminId)} is not be empty!");
            }

            if (string.IsNullOrWhiteSpace(nickname))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<CourseAdmin>($"{nameof(nickname)} is not be null or whitespace"));
            }

            if (!string.IsNullOrWhiteSpace(nickname)
                && nickname.Length > MaxLengthNickname)
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<CourseAdmin>($"{nameof(nickname)} is not be more than {MaxLengthNickname} chars"));
            }

            if (failure.IsFailure)
            {
                return Result.Failure<CourseAdmin>(failure.Error);
            }

            var courseAdmin = new CourseAdmin(courseAdminId, nickname);

            return courseAdmin;
        }
    }
}
