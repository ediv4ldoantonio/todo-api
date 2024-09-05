using Todo.API.Models;

namespace Todo.API.Repositories.Contracts;

public interface ITodoItemsRepository : IRepository<TodoItem>
{
    Task<IEnumerable<TodoItem>> GetAllByCategoryAsync(string categoryId);
}