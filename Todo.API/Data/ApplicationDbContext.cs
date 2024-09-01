using Microsoft.EntityFrameworkCore;
using Todo.API.Models;

namespace Todo.API.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
}