using Microsoft.Extensions.DependencyInjection;
using Todo.API.DataContext;
using Todo.API.Enums;
using Todo.API.Models;
using Todo.API.Repositories;

namespace Todo.API.Tests.Fixtures;

public class TodoItemRepository_Tests : IClassFixture<BaseFixture>
{
    private readonly TodoItemRepository todoItemRepository;
    private readonly ApplicationDbContext appDbContext;
    private readonly BaseFixture baseFixture;

    public TodoItemRepository_Tests(BaseFixture baseFixture)
    {
        this.baseFixture = baseFixture;
        todoItemRepository = baseFixture.ServiceProvider.GetRequiredService<TodoItemRepository>();
        appDbContext = baseFixture.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    private TodoItem GetTodoItem()
    {
        return new()
        {
            Title = "Test",
            Description = "desc",
            DueDate = DateTime.Now.AddDays(5),
            Priority = Priority.High,
            Status = Status.Pending
        };
    }

    [Fact]
    public async Task Test_Should_Create_TodoItem()
    {
        await baseFixture.CleanDatabase();

        TodoItem todoItem = GetTodoItem();

        await todoItemRepository.AddAsync(todoItem);

        List<TodoItem> todoItems = appDbContext.TodoItems.ToList();
        Assert.Single(todoItems);
        Assert.Equal(todoItem.Description, todoItems[0].Description);
        Assert.Equal(todoItem.Id, todoItems[0].Id);
        Assert.Equal(todoItem.Title, todoItems[0].Title);
    }

    [Fact]
    public async Task Test_Should_Delete_TodoItem()
    {
        await baseFixture.CleanDatabase();

        TodoItem todoItem = GetTodoItem();

        appDbContext.TodoItems.Add(todoItem);

        await appDbContext.SaveChangesAsync();

        var todoItems = appDbContext.TodoItems.ToList();

        Assert.NotEmpty(todoItems);

        await todoItemRepository.DeleteAsync(todoItem.Id);

        todoItems = appDbContext.TodoItems.ToList();

        Assert.Empty(todoItems);
    }

    [Fact]
    public async Task Test_Should_Get_All_TodoItems()
    {
        await baseFixture.CleanDatabase();

        for (int i = 0; i < 3; i++)
            appDbContext.TodoItems.Add(GetTodoItem());

        await appDbContext.SaveChangesAsync();

        IEnumerable<TodoItem> todoItems = await todoItemRepository.GetAllAsync();
        Assert.NotEmpty(todoItems);
        Assert.Equal(3, todoItems.Count());
    }

    [Fact]
    public async Task Test_Should_Get_TodoItem_By_Id()
    {
        await baseFixture.CleanDatabase();

        TodoItem todoItem = GetTodoItem();

        appDbContext.TodoItems.Add(todoItem);

        await appDbContext.SaveChangesAsync();

        TodoItem? retrievedTodoItem = await todoItemRepository.GetByIdAsync(todoItem.Id);

        Assert.NotNull(retrievedTodoItem);
        Assert.Equal(todoItem.Id, retrievedTodoItem.Id);
        Assert.Equal(todoItem.Title, retrievedTodoItem.Title);
    }

    [Fact]
    public async Task Test_Should_Update_TodoItem()
    {
        await baseFixture.CleanDatabase();

        TodoItem todoItem = GetTodoItem();

        appDbContext.TodoItems.Add(todoItem);

        await appDbContext.SaveChangesAsync();

        todoItem.Title = "changed test";

        await todoItemRepository.UpdateAsync(todoItem);

        TodoItem? updatedTodoItem = appDbContext.TodoItems
                .Where(item => item.Id == todoItem.Id)
                .FirstOrDefault();

        Assert.Equal(todoItem.Id, updatedTodoItem!.Id);
        Assert.Equal("changed test", updatedTodoItem!.Title);
    }
}