using LeagueWiki.Models;
using Microsoft.EntityFrameworkCore;

namespace LeagueWiki.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Champion> Champions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
