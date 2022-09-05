using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain
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
            var failureResults = new List<Result<CourseAdmin>>();
            if (courseAdminId == Guid.Empty)
            {
                var failure = Result.Failure<CourseAdmin>(
                    $"{nameof(courseAdminId)} is not be empty!");
                failureResults.Add(failure);
            }

            if (string.IsNullOrWhiteSpace(nickname))
            {
                var failure = Result.Failure<CourseAdmin>(
                    $"{nameof(nickname)} is not be null or whitespace");
                failureResults.Add(failure);
            }

            if (!string.IsNullOrWhiteSpace(nickname)
                && nickname.Length > MaxLengthNickname)
            {
                var failure = Result.Failure<CourseAdmin>(
                    $"{nameof(nickname)} is not be more than {MaxLengthNickname} chars");
                failureResults.Add(failure);
            }

            if (failureResults.Any())
            {
                var erros = Result.Combine(failureResults, "  ").Error;
                return Result.Failure<CourseAdmin>(erros);
            }

            var courseAdmin = new CourseAdmin(courseAdminId, nickname);

            return courseAdmin;
        }
    }
}
