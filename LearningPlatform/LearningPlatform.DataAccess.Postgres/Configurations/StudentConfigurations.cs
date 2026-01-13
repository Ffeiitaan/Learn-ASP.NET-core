using LearningPlatform.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.DataAccess.Postgers.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
{
    public void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.
            HasMany(s => s.Courses)
            .WithMany(c => c.Students);
    }
}