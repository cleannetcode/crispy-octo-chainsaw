using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Model
{
    public record Course
    {
        public const int MaxTitleLength = 500;
        public const int MaxDescriptionsLength = 1500;
        public const int MaxRepositoryNameLength = 50;

        public int Id { get; init; }
        public string Title { get; }
        public string Description { get; }
        public string RepositoryName { get; }
        public Guid CourseAdminId { get; }

        public Exercise[] Exercises { get; set; }

        private Course(int id, string title, string description, string repositoryName)
        {
            Id = id;
            Title = title;
            Description = description;
            RepositoryName = repositoryName;
        }

        public static Result<Course> Create(string title, string description, string repositoryName)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Failure<Course>(Errors.Errors.Title.TitleCanNotBeNullOrWhiteSpace);
            }

            if (string.IsNullOrWhiteSpace(repositoryName))
            {
                return Result.Failure<Course>("RepositoryName не может быть пустым или null");
            }

            if (title.Length > MaxTitleLength)
            {
                var error = string.Format(Errors.Errors.Title.TitleMaxLength, MaxTitleLength);
                return Result.Failure<Course>(error);
            }

            if (repositoryName.Length > MaxRepositoryNameLength)
            {
                return Result.Failure<Course>($"RepositoryName не может быть больше {MaxRepositoryNameLength} символов");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Failure<Course>(Errors.Errors.Descriptions.DescriptionsCanNotBeNullOrWhiteSpace);
            }

            if (description.Length > MaxDescriptionsLength)
            {
                var error = string.Format(Errors.Errors.Descriptions.DescriptionsMaxLength, MaxDescriptionsLength);
                return Result.Failure<Course>(error);
            }

            return new Course(0, title, description, repositoryName);
        }
    }
}
