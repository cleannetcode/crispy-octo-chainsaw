using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrispyOctoChainsaw.DataAccess.Postgre
{
    public class CrispyOctoChainsawDbContext : IdentityDbContext
    {
        public CrispyOctoChainsawDbContext(DbContextOptions<CrispyOctoChainsawDbContext> options)
            : base(options)
        {
        }
    }
}