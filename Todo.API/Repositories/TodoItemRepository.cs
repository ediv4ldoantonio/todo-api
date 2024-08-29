using Microsoft.EntityFrameworkCore;
using Todo.API.DataContext;
using Todo.API.Models;
using Todo.API.Repositories.Contracts;

namespace Todo.API.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly ApplicationDbContext appDbContext;

    public TodoItemRepository(ApplicationDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task AddAsync(TodoItem todoItem)
    {
        appDbContext.TodoItems.Add(todoItem);

        await appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        TodoItem? todoItem = await GetByIdAsync(id)
                ?? throw new Exception("TodoItem not found");

        appDbContext.TodoItems.Remove(todoItem);

        await appDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await appDbContext.TodoItems
            .ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(string id)
    {
        return await appDbContext.TodoItems
            .Where(todoItem => todoItem.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(TodoItem todoItem)
    {
        appDbContext.TodoItems.Update(todoItem);

        await appDbContext.SaveChangesAsync();
    }
}