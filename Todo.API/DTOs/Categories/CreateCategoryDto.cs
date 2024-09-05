using System.ComponentModel.DataAnnotations;

namespace Todo.API.DTOs.Categories;

public record CreateCategoryDto
{
    [Required(ErrorMessage = "Please enter the Name")]
    public required string Name { get; set; }
    public string? Description { get; set; }
}