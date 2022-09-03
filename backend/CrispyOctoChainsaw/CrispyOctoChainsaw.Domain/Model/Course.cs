using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Model
{
    public record Course
    {
        public long Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string RepositoryName { get; }
        public Guid CourseAdminId { get; }

        public Exercise[] Exercises { get; set; }

        private Course(long id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public static Result<Course> Create(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Failure<Course>("Title не может быть пустым");
            }

            return new Course(0, title, description);
        }
    }
}
