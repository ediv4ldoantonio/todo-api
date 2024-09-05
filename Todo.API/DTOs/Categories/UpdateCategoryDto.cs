namespace Todo.API.DTOs.Categories;

public record UpdateCategoryDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}