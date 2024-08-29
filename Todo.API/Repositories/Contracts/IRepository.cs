namespace Todo.API.Repositories.Contracts;

public interface IRepository<T> where T : class
{
    // Get all items
    Task<IEnumerable<T>> GetAllAsync();

    // Get a single item by ID
    Task<T?> GetByIdAsync(string id);

    // Add a new item
    Task AddAsync(T entity);

    // Update an existing item
    Task UpdateAsync(T entity);

    // Delete an item by ID
    Task DeleteAsync(string id);
}
