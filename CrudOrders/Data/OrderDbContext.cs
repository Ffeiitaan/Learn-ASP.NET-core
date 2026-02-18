using Microsoft.EntityFrameworkCore;
using CrudOrders.Entities;

namespace CrudOrders.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
    }
}