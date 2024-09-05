using Todo.API.Enums;

namespace Todo.API.DTOs.TodoItems;

public record TodoItemDto
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string CategoryId { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}