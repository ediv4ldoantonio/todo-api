using Todo.API.DTOs;
using Todo.API.Models;
using Todo.API.Repositories.Contracts;
using Todo.API.Services.Contracts;

namespace Todo.API.Services;

public class TodoItemsService : ITodoItemsService
{
    private readonly ITodoItemsRepository todoItemsRepository;

    public TodoItemsService(ITodoItemsRepository todoItemsRepository)
    {
        this.todoItemsRepository = todoItemsRepository;
    }
    public async Task<TodoItemDto> AddAsync(CreateTodoItemDto createTodoItem)
    {
        TodoItem todoItem = new()
        {
            Title = createTodoItem.Title,
            Description = createTodoItem.Description,
            DueDate = createTodoItem.DueDate,
            Priority = createTodoItem.Priority,
        };

        await todoItemsRepository.AddAsync(todoItem);

        return new TodoItemDto()
        {
            Id = todoItem.Id,
            Description = todoItem.Description,
            Title = todoItem.Title,
            DueDate = todoItem.DueDate,
            CreatedAt = todoItem.CreatedAt,
            Priority = todoItem.Priority,
            Status = todoItem.Status,
            UpdatedAt = todoItem.UpdatedAt
        };
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TodoItemDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TodoItemDto?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TodoItemDto todoItemDto)
    {
        throw new NotImplementedException();
    }
}