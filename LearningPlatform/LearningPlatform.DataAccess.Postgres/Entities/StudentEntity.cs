namespace LearningPlatform.DataAccess.Postgres.Entities;


public class StudentEntity
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public List<CourseEntity> Courses { get; set; } = [];
}