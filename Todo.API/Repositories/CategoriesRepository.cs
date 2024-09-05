using Microsoft.EntityFrameworkCore;
using Todo.API.Data;
using Todo.API.Models;
using Todo.API.Repositories.Contracts;

namespace Todo.API.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly ApplicationDbContext appDbContext;

    public CategoriesRepository(ApplicationDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task AddAsync(Category category)
    {
        appDbContext.Categories.Add(category);

        await appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        Category category = await GetByIdAsync(id)
                ?? throw new Exception("Category not found");

        appDbContext.Categories.Remove(category);

        await appDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await appDbContext.Categories
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(string id)
    {
        return await appDbContext.Categories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        appDbContext.Categories.Update(category);

        await appDbContext.SaveChangesAsync();
    }
}