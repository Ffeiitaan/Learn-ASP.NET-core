using LearningPlatform.DataAccess.Postgers;
using LearningPlatform.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.DataAccess.Postgres.Repositories;

public class LessonsRepository
{
    private readonly LearningDbContext _dbContext;

    public LessonsRepository(LearningDbContext context)
    {
        _dbContext = context;
    }

    public async Task Add(Guid courseId, LessonEntity lesson)
    {
        var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId)
            ?? throw new Exception();

        course.Lessons.Add(lesson);
    }
}