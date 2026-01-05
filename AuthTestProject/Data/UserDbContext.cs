using AuthTestProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthTestProject.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}