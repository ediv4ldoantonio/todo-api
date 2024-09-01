using Todo.API.Enums;
using Todo.API.Models;

namespace Todo.API.Data;

public static class DataSeeder
{
    public async static Task Seed(ApplicationDbContext appDbContext)
    {
        if (!appDbContext.TodoItems.Any())
        {
            appDbContext.AddRange([
                new TodoItem()
                {
                    Title = "Make an API",
                    Description = "Build an API with ASP.NET CORE",
                    DueDate = DateTime.Now.AddDays(3),
                    Priority = Priority.High,
                    Status = Status.Pending
                },
                new TodoItem()
                {
                    Title = "Build the Frontend",
                    Description = "Build the frontend to consume the API",
                    DueDate = DateTime.Now.AddDays(-3),
                    Priority = Priority.Low,
                    Status = Status.Completed
                },
                new TodoItem()
                {
                    Title = "Build a Test Project",
                    Description = "A test project for functional tests",
                    DueDate = DateTime.Now.AddDays(3),
                    Priority = Priority.Medium,
                    Status = Status.InProgress
                }
            ]);

            await appDbContext.SaveChangesAsync();
        }

    }
}