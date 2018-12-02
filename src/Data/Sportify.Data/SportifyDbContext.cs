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

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Sport> Sports { get; set; }
        
        public DbSet<Town> Towns { get; set; }
        
        public DbSet<Venue> Venues { get; set; }
    }
}