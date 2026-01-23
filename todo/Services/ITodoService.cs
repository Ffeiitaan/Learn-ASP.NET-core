using Todo.Entities;
using Todo.Models;

namespace Todo.Services;

public interface ITodoService
{
    Task<TodoEntity> TodoAdd(TodoDto request);
}