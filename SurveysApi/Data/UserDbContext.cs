using Microsoft.EntityFrameworkCore;
using SurveysApi.Models;

namespace SurveysApi.Data;
    public class UserDbContext : DbContext
    {
        public UserDbContext (DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;
    }
