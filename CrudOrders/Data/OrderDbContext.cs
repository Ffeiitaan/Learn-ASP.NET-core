using Microsoft.EntityFrameworkCore;
using CrudOrders.Entities;

namespace CrudOrders.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options){}

        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
    }
}