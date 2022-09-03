using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain.Model
{
    public record Course
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RepositoryName { get; set; }
        public long AuthorId { get; set; }
        public string Status { get; set; }

        public IEnumerable<Exercise> Exercises { get; set; }

        public Course(long id, string title, string description)
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

    public record Exercise
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BranchName { get; set; }
    }
}
