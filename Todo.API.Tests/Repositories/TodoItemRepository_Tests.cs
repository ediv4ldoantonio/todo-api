using Microsoft.Extensions.DependencyInjection;
using Todo.API.DataContext;
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
    public void Test_Should_Create_TodoItem()
    {
        Assert.Fail("Implement this");
    }
}