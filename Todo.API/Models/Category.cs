using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models;

public class Category
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}