
namespace LearningPlatform.DataAccess.Postgres.Entities;


public class AuthorEntity
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public Guid CourseId { get; set; }

    public CourseEntity? Course { get; set; }
}