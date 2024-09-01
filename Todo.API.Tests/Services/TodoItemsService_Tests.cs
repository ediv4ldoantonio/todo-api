using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.API.Data;
using Todo.API.DTOs;
using Todo.API.Enums;
using Todo.API.Models;
using Todo.API.Services;
using Todo.API.Tests.Factories;
using Todo.API.Tests.Fixtures;

namespace Todo.API.Tests.Services;

public class TodoItemsService_Tests : IClassFixture<BaseFixture>
{
    private readonly TodoItemsService todoItemsService;
    private readonly TodoItemsFactory todoItemsFactory;
    private readonly ApplicationDbContext appDbContext;
    private readonly BaseFixture baseFixture;

    public TodoItemsService_Tests(BaseFixture baseFixture)
    {
        this.baseFixture = baseFixture;
        todoItemsService = baseFixture.ServiceProvider.GetRequiredService<TodoItemsService>();
        todoItemsFactory = baseFixture.ServiceProvider.GetRequiredService<TodoItemsFactory>();
        appDbContext = baseFixture.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    [Fact]
    public async Task Test_Should_Create_TodoItem()
    {
        await baseFixture.CleanDatabase();

        TodoItemDto todoItemDto = await todoItemsService.AddAsync(new CreateTodoItemDto()
        {
            Title = "Todo-1",
            Description = "lorem ipsum",
            DueDate = DateTime.Today.AddDays(7),
            Priority = Priority.Low
        });

        var todoItems = await appDbContext.TodoItems.ToListAsync();

        Assert.Single(todoItems);
        Assert.NotNull(todoItemDto);
        Assert.NotNull(todoItemDto.Id);
        Assert.Equal(todoItemDto.Title, todoItems[0].Title);
        Assert.Equal(todoItemDto.Description, todoItems[0].Description);
        Assert.Equal(todoItemDto.DueDate, todoItems[0].DueDate);
        Assert.Equal(todoItemDto.Priority, todoItems[0].Priority);
        Assert.Equal(todoItemDto.Status, todoItems[0].Status);
    }

    [Fact]
    public async Task Test_Should_Delete_TodoItem()
    {
        await baseFixture.CleanDatabase();

        TodoItem todoItem = todoItemsFactory.GetTodoItem();

        appDbContext.TodoItems.Add(todoItem);

        await appDbContext.SaveChangesAsync();

        List<TodoItem> todoItems = await appDbContext.TodoItems.ToListAsync();

        Assert.NotEmpty(todoItems);

        await todoItemsService.DeleteAsync(todoItem.Id);

        todoItems = await appDbContext.TodoItems.ToListAsync();

        Assert.Empty(todoItems);
    }

    [Fact]
    public async Task Test_Should_Get_All_TodoItems()
    {
        await baseFixture.CleanDatabase();

        List<TodoItem> todoItems = [];

        for (int i = 0; i < 3; i++)
            todoItems.Add(todoItemsFactory.GetTodoItem());

        appDbContext.TodoItems.AddRange(todoItems);

        await appDbContext.SaveChangesAsync();

        IEnumerable<TodoItemDto> retrievedTodoItems = await todoItemsService.GetAllAsync();

        Assert.NotEmpty(retrievedTodoItems);
        Assert.Equal(3, retrievedTodoItems.Count());
    }

    [Fact]
    public async Task Test_Should_Get_TodoItem_By_Id()
    {
        await baseFixture.CleanDatabase();

        TodoItem todoItem = todoItemsFactory.GetTodoItem();

        appDbContext.TodoItems.Add(todoItem);

        await appDbContext.SaveChangesAsync();

        TodoItemDto? retrievedTodoItemDto = await todoItemsService.GetByIdAsync(todoItem.Id);

        Assert.NotNull(retrievedTodoItemDto);
        Assert.Equal(todoItem.Id, retrievedTodoItemDto.Id);
        Assert.Equal(todoItem.Title, retrievedTodoItemDto.Title);
        Assert.Equal(todoItem.Description, retrievedTodoItemDto.Description);
    }
}