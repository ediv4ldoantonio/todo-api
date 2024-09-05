using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models;

public class Category
{
    [Key]
    public required string Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}