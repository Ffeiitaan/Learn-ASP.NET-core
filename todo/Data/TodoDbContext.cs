using Microsoft.EntityFrameworkCore;
using Todo.Entities;

namespace Todo.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options){ }

    public DbSet<TodoEntity> Todos => Set<TodoEntity>();
}