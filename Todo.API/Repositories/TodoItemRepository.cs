using Todo.API.DataContext;
using Todo.API.Models;
using Todo.API.Repositories.Contracts;

namespace Todo.API.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly ApplicationDbContext applicationDbContext;


    public TodoItemRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public Task AddAsync(TodoItem entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TodoItem entity)
    {
        throw new NotImplementedException();
    }
}