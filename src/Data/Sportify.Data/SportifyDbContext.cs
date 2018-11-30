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
    }
}