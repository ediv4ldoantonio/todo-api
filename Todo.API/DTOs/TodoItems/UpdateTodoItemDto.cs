using Todo.API.Enums;

namespace Todo.API.DTOs.TodoItems;

public record UpdateTodoItemDto
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public DateTime DueDate { get; set; }

    public Priority Priority { get; set; }
    public Status Status { get; set; }
}