using Microsoft.EntityFrameworkCore;

namespace Todo.API.DataContext;

public class AppDataContext : DbContext
{
    public DbSet<Models.Task> Tasks { get; set; }

    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {

    }
}