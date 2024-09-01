using Todo.API.DTOs;

namespace Todo.API.Services.Contracts;

public interface ITodoItemsService
{
    // Get all TodoItems items
    Task<IEnumerable<TodoItemDto>> GetAllAsync();

    // Get a single TodoItem by ID
    Task<TodoItemDto?> GetByIdAsync(string id);

    // Add a new TodoItem
    Task AddAsync(CreateTodoItemDto createTodoItem);

    // Update an existing TodoItem
    Task UpdateAsync(TodoItemDto todoItemDto);

    // Delete an iTodoItem by ID
    Task DeleteAsync(string id);
}