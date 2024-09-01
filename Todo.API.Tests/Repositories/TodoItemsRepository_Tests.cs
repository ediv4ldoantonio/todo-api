using Microsoft.Extensions.DependencyInjection;
using Todo.API.Data;
using Todo.API.Models;
using Todo.API.Repositories;
using Todo.API.Tests.Factories;

namespace Todo.API.Tests.Fixtures;

public class TodoItemsRepository_Tests : IClassFixture<BaseFixture>
{
    private readonly TodoItemsRepository todoItemsRepository;
    private readonly TodoItemsFactory todoItemsFactory;
    private readonly ApplicationDbContext appDbContext;
    private readonly BaseFixture baseFixture;

    public TodoItemsRepository_Tests(BaseFixture baseFixture)
    {
        this.baseFixture = baseFixture;
        todoItemsFactory = baseFixture.ServiceProvider.GetRequiredService<TodoItemsFactory>();
        todoItemsRepository = baseFixture.ServiceProvider.GetRequiredService<TodoItemsRepository>();
        appDbContext = baseFixture.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    [Fact]
    public async Task Test_Should_Create_TodoItem()
    {
        await baseFixture.CleanDatabase();

        TodoItem todoItem = todoItemsFactory.GetTodoItem();

        await todoItemsRepository.AddAsync(todoItem);

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

        TodoItem todoItem = todoItemsFactory.GetTodoItem();

        appDbContext.TodoItems.Add(todoItem);

        await appDbContext.SaveChangesAsync();

        var todoItems = appDbContext.TodoItems.ToList();

        Assert.NotEmpty(todoItems);

        await todoItemsRepository.DeleteAsync(todoItem.Id);

        todoItems = appDbContext.TodoItems.ToList();

        Assert.Empty(todoItems);
    }

    [Fact]
    public async Task Test_Should_Get_All_TodoItems()
    {
        await baseFixture.CleanDatabase();

        for (int i = 0; i < 3; i++)
            appDbContext.TodoItems.Add(todoItemsFactory.GetTodoItem());

        await appDbContext.SaveChangesAsync();

        IEnumerable<TodoItem> todoItems = await todoItemsRepository.GetAllAsync();
        Assert.NotEmpty(todoItems);
        Assert.Equal(3, todoItems.Count());
    }

    [Fact]
    public async Task Test_Should_Get_TodoItem_By_Id()
    {
        await baseFixture.CleanDatabase();

        TodoItem todoItem = todoItemsFactory.GetTodoItem();

        appDbContext.TodoItems.Add(todoItem);

        await appDbContext.SaveChangesAsync();

        TodoItem? retrievedTodoItem = await todoItemsRepository.GetByIdAsync(todoItem.Id);

        Assert.NotNull(retrievedTodoItem);
        Assert.Equal(todoItem.Id, retrievedTodoItem.Id);
        Assert.Equal(todoItem.Title, retrievedTodoItem.Title);
    }

    [Fact]
    public async Task Test_Should_Update_TodoItem()
    {
        await baseFixture.CleanDatabase();

        TodoItem todoItem = todoItemsFactory.GetTodoItem();

        appDbContext.TodoItems.Add(todoItem);

        await appDbContext.SaveChangesAsync();

        todoItem.Title = "changed test";

        await todoItemsRepository.UpdateAsync(todoItem);

        TodoItem? updatedTodoItem = appDbContext.TodoItems
                .Where(item => item.Id == todoItem.Id)
                .FirstOrDefault();

        Assert.Equal(todoItem.Id, updatedTodoItem!.Id);
        Assert.Equal("changed test", updatedTodoItem!.Title);
    }
}