namespace CrispyOctoChainsaw.DataAccess.Postgres.Entities
{
    public record CourseEntity
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RepositoryName { get; set; }
        public long AuthorId { get; set; }
        public string Status { get; set; }

        public CourseEntity(long id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}
