using CrispyOctoChainsaw.Domain.Errors;
using CrispyOctoChainsaw.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace CrispyOctoChainsaw.DataAccess.Postgres.Entities
{
    public class CourseEntity
    {
        public int Id { get; set; }

        [StringLength(Course.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(Course.MaxDescriptionsLength)]
        public string Description { get; set; }

        [StringLength(Course.MaxRepositoryNameLength)]
        public string RepositoryName { get; set; }

        public Guid CourseAdminId { get; set; }
    }
}
