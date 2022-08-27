using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrispyOctoChainsaw.DataAccess.Postgres
{
    public class CrispyOctoChainsawDbContext : IdentityDbContext
    {
        public CrispyOctoChainsawDbContext(DbContextOptions<CrispyOctoChainsawDbContext> options)
            : base(options)
        {
        }
    }
}