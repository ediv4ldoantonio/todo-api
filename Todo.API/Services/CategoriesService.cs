using Todo.API.DTOs;
using Todo.API.DTOs.Categories;
using Todo.API.Services.Contracts;

namespace Todo.API.Services;

public class CategoriesService : ICategoriesService
{
    public Task<CategoryDto> AddAsync(CreateCategoryDto createCategoryDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CategoryDto?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryDto> UpdateAsync(CategoryDto categoryDto)
    {
        throw new NotImplementedException();
    }
}