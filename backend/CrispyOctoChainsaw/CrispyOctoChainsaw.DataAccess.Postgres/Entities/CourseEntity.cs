using CrispyOctoChainsaw.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        [StringLength(Course.MaxBannerNameLength)]
        public string BannerName { get; set; }

        public DateTimeOffset? DeleteTime { get; set; }

        public Guid CourseAdminId { get; set; }

        public ICollection<ExerciseEntity> Exercises { get; set; } = new List<ExerciseEntity>();
    }

    public class CourseEntityConfiguration : IEntityTypeConfiguration<CourseEntity>
    {
        public void Configure(EntityTypeBuilder<CourseEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .IsRequired(true);

            builder.Property(x => x.CourseAdminId)
                .IsRequired(true);

            builder.Property(x => x.RepositoryName)
                .IsRequired(true);

            builder.Property(x => x.BannerName)
                .IsRequired(true);

            builder.Property(x => x.DeleteTime)
                .IsRequired(false);
        }
    }
}
