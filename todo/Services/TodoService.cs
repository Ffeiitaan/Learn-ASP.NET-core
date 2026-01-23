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

        var todo = new TodoEntity();

        todo.Title = request.Title;
        todo.Description = request.Description;

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return todo;
    }
}