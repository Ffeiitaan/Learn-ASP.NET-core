using System.ComponentModel.DataAnnotations;

namespace Todo.Models;

public class UpdateTodoTextDto
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set;} = string.Empty;
    public string Description { get; set;} = string.Empty;
}