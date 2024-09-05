using Todo.API.Models;
using Todo.API.Repositories.Contracts;

namespace Todo.API.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    public Task AddAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Category entity)
    {
        throw new NotImplementedException();
    }
}