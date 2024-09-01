using System.ComponentModel.DataAnnotations;
using Todo.API.Enums;

namespace Todo.API.DTOs;

public class CreateTodoItemDto
{
    [Required]
    public required string Title { get; set; }

    public string? Description { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public Priority Priority { get; set; }
}