using Microsoft.Extensions.DependencyInjection;
using Todo.API.DataContext;
using Todo.API.Enums;
using Todo.API.Models;
using Todo.API.Repositories;

namespace Todo.API.Tests.Fixtures;

public class TodoItemRepository_Tests : IClassFixture<BaseFixture>
{
    private readonly TodoItemRepository todoItemRepository;
    private readonly ApplicationDbContext applicationDbContext;

    public TodoItemRepository_Tests(BaseFixture baseFixture)
    {
        todoItemRepository = baseFixture.ServiceProvider.GetRequiredService<TodoItemRepository>();
        applicationDbContext = baseFixture.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    [Fact]
    public async Task Test_Should_Create_TodoItem()
    {
        TodoItem todoItem = new()
        {
            Title = "Test",
            Description = "desc",
            DueDate = DateTime.Now.AddDays(5),
            Priority = Priority.High,
            Status = Status.Pending
        };

        await todoItemRepository.AddAsync(todoItem);

        List<TodoItem> todoItems = applicationDbContext.TodoItems.ToList();

        Assert.NotEmpty(todoItems);
        Assert.Single(todoItems);
        Assert.Equal(todoItem.Description, todoItems[0].Description);
        Assert.Equal(todoItem.Id, todoItems[0].Id);
        Assert.Equal(todoItem.Title, todoItems[0].Title);
        Assert.Equal(todoItem.Status, todoItems[0].Status);
        Assert.Equal(todoItem.DueDate, todoItems[0].DueDate);
    }
}