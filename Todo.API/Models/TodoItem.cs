using System.ComponentModel.DataAnnotations;
using Todo.API.Enums;

namespace Todo.API.Models;

public class TodoItem
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public required string Title { get; set; }
    public required Category Category { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; } = Status.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}