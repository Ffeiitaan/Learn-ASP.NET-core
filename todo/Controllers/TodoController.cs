using Microsoft.AspNetCore.Mvc;
using Todo.Entities;
using Todo.Models;
using Todo.Services;

namespace Todo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController(ITodoService todoService) : ControllerBase
{
// Додати задачу.
    [HttpPost("add-todo")]
    public async Task<ActionResult> TodoAdd(TodoDto request)
    {
        try
        {
            var todo = await todoService.TodoAdd(request);

            return Ok(todo);
        }
        catch(InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch(ArgumentException ex)
        {
            return Conflict(ex.Message);
        }
    }

// Оновити задачі.
    [HttpPut("update-todo-to-done")]
    public async Task<ActionResult> UpdateTodoToDone(UpdateTodoStatusDto request)
    {
        try
        {
            var todoDone = await todoService.TodoDone(request);

            return Ok(todoDone);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("update-todo-to-canceled")]
    public async Task<ActionResult> UpdateTodoToCanceled(UpdateTodoStatusDto request)
    {
        try
        {
            var todoCanceled = await todoService.TodoCanceled(request);

            return Ok(todoCanceled);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("update-todo")]
    public async Task<ActionResult> UpdateTodo(UpdateTodoTextDto request)
    {
        try
        {
            var updateTodo = await todoService.UpdateTodo(request);

            return Ok(updateTodo);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

// Видалити задачу.
    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveTodo(Guid id)
    {
        try
        {
            await todoService.RemoveTodo(id);

            return NoContent();
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


// Показати задчі.
    [HttpGet]
    public async Task<ActionResult<List<TodoEntity>>> GetAllTodos()
    {
        var todos = await todoService.GetAllTodos();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoEntity>> GetById(Guid id)
    {
        try
        {
            var todo = await todoService.GetById(id);
            return Ok(todo);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}