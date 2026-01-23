using Todo.Entities;
using Todo.Models;

namespace Todo.Services;

public interface ITodoService
{
    // Створення.
    Task<TodoEntity> TodoAdd(TodoDto request);

    // Оновлення статусу.
    Task<TodoEntity> TodoDone(UpdateTodoStatusDto request);
    Task<TodoEntity> TodoCanceled(UpdateTodoStatusDto request);

    // Редагування тексту.
    Task<TodoEntity> UpdateTodo(UpdateTodoTextDto request);

    // Видалення.
    Task RemoveTodo(Guid id);

    // Показати.
    Task<List<TodoEntity>> GetAllTodos();
    Task<TodoEntity> GetById(Guid id);
}