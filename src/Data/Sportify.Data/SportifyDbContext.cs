namespace Sportify.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class SportifyDbContext : IdentityDbContext<User>
    {
        public SportifyDbContext(DbContextOptions<SportifyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Venue> Venues { get; set; }
    }
}