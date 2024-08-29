using Microsoft.EntityFrameworkCore;
using Todo.API.Models;

namespace Todo.API.DataContext;

public class AppDataContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {

    }
}