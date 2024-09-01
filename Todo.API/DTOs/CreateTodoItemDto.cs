using System.ComponentModel.DataAnnotations;
using Todo.API.Enums;

namespace Todo.API.DTOs;

public class CreateTodoItemDto
{
    [Required(ErrorMessage = "Please enter the Title")]
    public required string Title { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = "Please enter the due date")]
    public DateTime DueDate { get; set; }

    [Required(ErrorMessage = "Please enter the Priority")]
    public Priority Priority { get; set; }
}