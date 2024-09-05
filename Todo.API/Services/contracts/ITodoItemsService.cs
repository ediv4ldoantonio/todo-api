using Todo.API.DTOs;
using Todo.API.DTOs.TodoItems;

namespace Todo.API.Services.Contracts;

public interface ITodoItemsService
{
    // Get all TodoItems items
    Task<IEnumerable<TodoItemDto>> GetAllAsync();
    Task<IEnumerable<TodoItemDto>> GetAllByCategoryAsync(string categoryId);

    // Get a single TodoItem by ID
    Task<TodoItemDto?> GetByIdAsync(string id);

    // Add a new TodoItem
    Task<TodoItemDto> AddAsync(CreateTodoItemDto createTodoItemDto);

    // Update an existing TodoItem
    Task<TodoItemDto> UpdateAsync(string id, UpdateTodoItemDto updateTodoItemDto);

    // Delete an iTodoItem by ID
    Task DeleteAsync(string id);
}