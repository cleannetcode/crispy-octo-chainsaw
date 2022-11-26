using CrispyOctoChainsaw.DataAccess.Postgres.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrispyOctoChainsaw.DataAccess.Postgres
{
    public class CrispyOctoChainsawDbContext
        : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public CrispyOctoChainsawDbContext(DbContextOptions<CrispyOctoChainsawDbContext> options)
            : base(options)
        {
        }

        public DbSet<CourseEntity> Courses { get; set; }

        public DbSet<SessionEntity> Sessions { get; set; }

        public DbSet<ExerciseEntity> Exercises { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(CrispyOctoChainsawDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}