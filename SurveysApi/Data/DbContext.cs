using Microsoft.EntityFrameworkCore;
using SurveysApi.Models;

namespace SurveysApi.Data
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<Role> Role { get; set; } = default!;
        public DbSet<Status> Status { get; set; } = default!;
        public DbSet<Survey> Survey { get; set; } = default!;


    }

}
