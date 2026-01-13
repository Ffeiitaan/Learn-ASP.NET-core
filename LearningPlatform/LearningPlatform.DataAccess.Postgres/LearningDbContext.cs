using LearningPlatform.DataAccess.Postgers.Configurations;
using LearningPlatform.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.DataAccess.Postgers;

public class LearningDbContext : DbContext
{
    public LearningDbContext(DbContextOptions<LearningDbContext> options) : base(options)
    {     
    }

    public DbSet<CourseEntity> Courses => Set<CourseEntity>();
    public DbSet<LessonEntity> Lessons => Set<LessonEntity>();
    public DbSet<AuthorEntity> Authors => Set<AuthorEntity>();
    public DbSet<StudentEntity> Students => Set<StudentEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}