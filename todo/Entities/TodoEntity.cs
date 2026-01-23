namespace Todo.Entities;

public class TodoEntity
{
    public Guid Id { get; set; }
    public string Title {get; set; } = string.Empty;
    public string Description {get; set; } = string.Empty;
    public TodoStatus Status { get; set; } = TodoStatus.InProcess;
}