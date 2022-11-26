using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Model
{
    public record Exercise
    {
        public const int MaxTitleLength = 500;
        public const int MaxDescriptionsLength = 1500;
        public const int MaxBranchNameLength = 50;

        private Exercise(int id, string title, string description, string branchName, int courseId)
        {
            Id = id;
            Title = title;
            Description = description;
            BranchName = branchName;
            CourseId = courseId;
        }

        public int Id { get; init; }

        public string Title { get; }

        public string Description { get; }

        public string BranchName { get; }

        public int CourseId { get; }

        public static Result<Exercise> Create(
            string title,
            string description,
            string branchName,
            int courseId)
        {
            Result failure = Result.Success();
            if (string.IsNullOrWhiteSpace(title))
            {
                failure = Result.Failure<Exercise>($"Exercise {nameof(title)} can`t be null or white space");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Exercise>($"Exercise {nameof(description)} can`t be null or white space"));
            }

            if (string.IsNullOrWhiteSpace(branchName))
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Exercise>($"Exercise {nameof(branchName)} name can`t be null or white space"));
            }

            if (courseId <= 0)
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Exercise>($"Course can`t be 0 or less 0"));
            }

            if (!string.IsNullOrWhiteSpace(title) && title.Length > MaxTitleLength)
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Exercise>($"Exercise {nameof(title)} can`t be more than {MaxTitleLength} chars"));
            }

            if (!string.IsNullOrWhiteSpace(description) && description.Length > MaxDescriptionsLength)
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Exercise>(
                        $"Exercise {nameof(description)} can`t be more than {MaxDescriptionsLength} chars"));
            }

            if (!string.IsNullOrWhiteSpace(branchName)
                && branchName.Length > MaxBranchNameLength)
            {
                failure = Result.Combine(
                    failure,
                    Result.Failure<Exercise>(
                        $"Exercise {nameof(branchName)} can`t be more than {MaxBranchNameLength} chars"));
            }

            if (failure.IsFailure)
            {
                return Result.Failure<Exercise>(failure.Error);
            }

            return new Exercise(0, title, description, branchName, courseId);
        }
    }
}
