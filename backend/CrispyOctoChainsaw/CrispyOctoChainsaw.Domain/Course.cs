using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.Domain
{
    public record Course
    {
        public int Id { get; init; }
        public string Title { get; }
        public string Description { get; }
        public string RepositoryName { get; }
        public Guid CourseAdminId { get; }

        public Exercise[] Exercises { get; set; }

        private Course(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public static Result<Course> Create(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Failure<Course>("sss");
            }

            if (title.Length > 100)
            {
                var error = string.Format("sss");
                return Result.Failure<Course>(error);
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Failure<Course>("sss");
            }

            if (description.Length > 100)
            {
                var error = string.Format("sss");
                return Result.Failure<Course>(error);
            }

            return new Course(0, title, description);
        }
    }

    public class Exercise
    {

    }
}
