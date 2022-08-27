namespace CrispyOctoChainsaw.DataAccess.Postgres.Entities
{
    public class CourseEntity
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? RepositoryName { get; set; }

        public Guid CourseAdminId { get; set; }
    }
}
