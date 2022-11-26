using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Model
{
    public record Course
    {
        public const int MaxTitleLength = 500;
        public const int MaxDescriptionsLength = 1500;
        public const int MaxRepositoryNameLength = 50;
        public const int MaxBannerNameLength = 40;

        private Course(
            int id,
            string title,
            string description,
            string repositoryName,
            string bannerName,
            Guid courseAdminId,
            Exercise[] exercises)
        {
            Id = id;
            Title = title;
            Description = description;
            RepositoryName = repositoryName;
            BannerName = bannerName;
            CourseAdminId = courseAdminId;
            Exercises = exercises;
        }

        public int Id { get; init; }

        public string Title { get; }

        public string Description { get; }

        public string RepositoryName { get; }

        public string BannerName { get; }

        public Guid CourseAdminId { get; }

        public Exercise[] Exercises { get; set; }

        public static Result<Course> Create(
            string title,
            string description,
            string repositoryName,
            string bannerName,
            Guid courseAdminId)
        {
            Result failure = Result.Success();
            if (string.IsNullOrWhiteSpace(title))
            {
                failure = Result.Failure<Course>($"Course {nameof(title)} can`t be null or white space");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Course>($"Course {nameof(description)} can`t be null or white space"));
            }

            if (string.IsNullOrWhiteSpace(repositoryName))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Course>($"Course {nameof(repositoryName)} name can`t be null or white space"));
            }

            if (courseAdminId == Guid.Empty)
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Session>($"{nameof(courseAdminId)} is not be empty!"));
            }

            if (string.IsNullOrWhiteSpace(bannerName))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Course>($"Course {nameof(bannerName)} can`t be null or white space"));
            }

            if (!string.IsNullOrWhiteSpace(title) && title.Length > MaxTitleLength)
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Course>($"Course {nameof(title)} can`t be more than {MaxTitleLength} chars"));
            }

            if (!string.IsNullOrWhiteSpace(description) && description.Length > MaxDescriptionsLength)
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Course>(
                        $"Course {nameof(description)} can`t be more than {MaxDescriptionsLength} chars"));
            }

            if (!string.IsNullOrWhiteSpace(repositoryName)
                && repositoryName.Length > MaxRepositoryNameLength)
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Course>(
                        $"Course {nameof(repositoryName)} can`t be more than {MaxRepositoryNameLength} chars"));
            }

            if (!string.IsNullOrWhiteSpace(bannerName) && bannerName.Length > MaxBannerNameLength)
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Course>(
                        $"Course {nameof(bannerName)} can`t be more than {MaxBannerNameLength} chars"));
            }

            if (failure.IsFailure)
            {
                return Result.Failure<Course>(failure.Error);
            }

            return new Course(
                0,
                title,
                description,
                repositoryName,
                bannerName,
                courseAdminId,
                Array.Empty<Exercise>());
        }
    }
}
