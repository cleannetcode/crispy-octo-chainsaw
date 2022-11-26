using CrispyOctoChainsaw.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrispyOctoChainsaw.DataAccess.Postgres.Entities
{
    public class ExerciseEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string BranchName { get; set; }

        public CourseEntity? Course { get; set; }

        public int CourseId { get; set; }
    }

    public class ExerciseEntityConfiguration : IEntityTypeConfiguration<ExerciseEntity>
    {
        public void Configure(EntityTypeBuilder<ExerciseEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(Exercise.MaxTitleLength)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .HasMaxLength(Exercise.MaxDescriptionsLength)
                .IsRequired(true);

            builder.Property(x => x.BranchName)
                .HasMaxLength(Exercise.MaxBranchNameLength)
                .IsRequired(true);

            builder.Property(x => x.CourseId)
                .IsRequired(true);
        }
    }
}
