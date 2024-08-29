using Microsoft.EntityFrameworkCore;
using Todo.API.DataContext;
using Todo.API.Models;
using Todo.API.Repositories.Contracts;

namespace Todo.API.Repositories;

/// <summary>
/// A repository that performs CRUD operations on TodoItems.
/// </summary>
public class TodoItemRepository : ITodoItemRepository
{
    private readonly ApplicationDbContext appDbContext;

    public TodoItemRepository(ApplicationDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <summary>
    /// Add a TodoItem to the Database asynchronously.
    /// </summary>
    /// <param name="todoItem">The TodoItem to be added.</param>
    public async Task AddAsync(TodoItem todoItem)
    {
        appDbContext.TodoItems.Add(todoItem);

        await appDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Delete the specified TodoItem from the Database.
    /// </summary>
    /// <param name="id">The Id of the TodoItem to be deleted.</param>
    /// <exception cref="Exception">Throws when the specified TodoItem does not exist in the Database.</exception>
    public async Task DeleteAsync(string id)
    {
        TodoItem? todoItem = await GetByIdAsync(id)
                ?? throw new Exception("TodoItem not found");

        appDbContext.TodoItems.Remove(todoItem);

        await appDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Get all the TodoItems that are in the Database.
    /// </summary>
    /// <returns>A collection of TodoItems.</returns>
    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await appDbContext.TodoItems
            .ToListAsync();
    }

    /// <summary>
    /// Retrieve the specified TodoItem from the Database.
    /// </summary>
    /// <param name="id">The Id of the requested TodoItem</param>
    /// <returns>The specified TodoItem</returns>
    public async Task<TodoItem?> GetByIdAsync(string id)
    {
        return await appDbContext.TodoItems
            .Where(todoItem => todoItem.Id == id)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Updates the specified TodoItem.
    /// </summary>
    /// <param name="todoItem">The updated TodoItem</param>
    public async Task UpdateAsync(TodoItem todoItem)
    {
        appDbContext.TodoItems.Update(todoItem);

        await appDbContext.SaveChangesAsync();
    }
}