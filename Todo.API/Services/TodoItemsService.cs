using Todo.API.DTOs;
using Todo.API.Services.Contracts;

namespace Todo.API.Services;

public class TodoItemsService : ITodoItemsService
{
    public Task AddAsync(CreateTodoItemDto createTodoItem)
    {
        throw new NotImplementedException();
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