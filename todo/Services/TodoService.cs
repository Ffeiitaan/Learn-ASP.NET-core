using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Entities;
using Todo.Models;

namespace Todo.Services;


public class TodoService : ITodoService
{
    private readonly TodoDbContext _context;

    public TodoService(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<TodoEntity> TodoAdd(TodoDto request)
    {
        if (await _context.Todos.AnyAsync(t => t.Title == request.Title)) 
            throw new InvalidOperationException("Todo already exists");;

        if(string.IsNullOrEmpty(request.Title)) 
            throw new ArgumentException("Title error");

        var todo = new TodoEntity();

        todo.Title = request.Title;
        todo.Description = request.Description;

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return todo;
    }

    public async Task<TodoEntity> TodoDone(UpdateTodoStatusDto request)
    {
        var todo = await _context.Todos
            .FirstOrDefaultAsync(i => i.Id == request.Id);

        if(todo == null) throw new KeyNotFoundException("Not Found");
        
        todo.Status = TodoStatus.Done;

        await _context.SaveChangesAsync();

        return todo;
    }

    public async Task<TodoEntity> TodoCanceled(UpdateTodoStatusDto request)
    {
        var todo = await _context.Todos
            .FirstOrDefaultAsync(i => i.Id == request.Id);

        if(todo == null) throw new KeyNotFoundException("Not Found");
        
        todo.Status = TodoStatus.Canceled;

        await _context.SaveChangesAsync();

        return todo;
    }

    public async Task<TodoEntity> UpdateTodo(UpdateTodoTextDto request)
    {
        var todo = await _context.Todos
            .FirstOrDefaultAsync(i => i.Id == request.Id);

        if(todo == null) throw new KeyNotFoundException("Not Found");

        todo.Title = request.Title;
        todo.Description = request.Description;

        await _context.SaveChangesAsync();

        return todo;
    }

    public async Task RemoveTodo(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if(todo == null) throw new KeyNotFoundException("$Todo with ID {id} not found");

        _context.Todos.Remove(todo);

        await _context.SaveChangesAsync();
    }

    public async Task<List<TodoEntity>> GetAllTodos()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<TodoEntity> GetById(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if(todo == null) throw new KeyNotFoundException("$Todo with ID {id} not found");

        return todo;
    }
}