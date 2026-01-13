using LearningPlatform.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.DataAccess.Postgers.Repositories;

public class CoursesRepository
{
    private readonly LearningDbContext _dbContext;

    public CoursesRepository(LearningDbContext DbContext)
    {
        _dbContext = DbContext;
    }

    public async Task<List<CourseEntity>> Get() // Повернути курси.
    {
        return await _dbContext.Courses
            .AsNoTracking() // для оптимизації, не буде відслідковувати змінення.
            .OrderBy(c => c.Title)
            .ToListAsync();
    }

    public async Task<List<CourseEntity>> GetWithLessons() // Повернути курси разом із уроками.
    {
        return await _dbContext.Courses
            .AsNoTracking()
            .Include(c => c.Lessons) // Тепер разом із курсами виводяться уроки.
            .ToListAsync();
    }

    public async Task<CourseEntity?> GetById(Guid id) // Повернути курс по Id.
    {
        return await _dbContext.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    } 

    public async Task<List<CourseEntity>> GetByFilter(string title, decimal price) // Можна фільтрувати ціну за ціною, або за назвою.
    {
        var query = _dbContext.Courses.AsNoTracking();

        if (!string.IsNullOrEmpty(title)) // Перевірка чи `title` не пустий.
        {
            query = query.Where(c => c.Title.Contains(title));
        }
        
        if (price > 0)
        {
            query = query.Where(c => c.Price > price);
        }

        return await query.ToListAsync();
    } 

    public async Task<List<CourseEntity>> GetByPage(int page, int pageSize) // Пагінація
    {
        return await _dbContext.Courses
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    } 

    public async Task Add(Guid id, Guid authorId, string title, string description, decimal price) // Додавання нового ентіті курсу в базу
    {
        var courseEntity = new CourseEntity
        {
            Id = id,
            AuthorId = authorId,
            Title = title,
            Description = description,
            Price = price
        };

        await _dbContext.AddAsync(courseEntity);
        await _dbContext.SaveChangesAsync(); // Збереження оновлень
    }

                                                // Старий спосіб оновлення. Вимагає на одну операцію більше // 

    // public async Task Update(Guid id, string title, string description, decimal price)
    // {
    //     var courseEntity = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == id)
    //         ?? throw new Exception();

    //     courseEntity.Title = title;
    //     courseEntity.Description = description;
    //     courseEntity.Price = price;

    //     await _dbContext.SaveChangesAsync();
    // }

    public async Task Update(Guid id, string title, string description, decimal price) // Новий метод оновлення, кращий тим що для його роботи потрібно зробити меньше операцій. 
    // Працює через `SetPropety()` та `ExecuteUpdateAsync()`
    {
        await _dbContext.Courses.
            Where(c => c.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Title, title)
                .SetProperty(c => c.Description, description)
                .SetProperty(c => c.Price, price));
    }   

    public async Task Delete(Guid id) 
    {
        await _dbContext.Courses
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
    }
}