using LearningPlatform.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.DataAccess.Postgers.Configurations;


public class CourseConfiguration : IEntityTypeConfiguration<CourseEntity> // Конфігурація для правильної ініціалізації таблиць у бд.
{
    public void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.
            HasOne(c => c.Author)
            .WithOne(a => a.Course)
            .HasForeignKey<CourseEntity>(c => c.AuthorId);

        builder.
            HasMany(c => c.Lessons)
            .WithOne(l => l.Course)
            .HasForeignKey(l => l.CourseId);

        builder.
            HasMany(c => c.Students)
            .WithMany(s => s.Courses);
    }
}