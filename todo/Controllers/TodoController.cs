using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Services;

namespace Todo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController(ITodoService todoService) : ControllerBase
{
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
    }
}