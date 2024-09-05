using Todo.API.DTOs;
using Todo.API.DTOs.Categories;

namespace Todo.API.Services.Contracts;

public interface ICategoriesService
{
    // Get all Category items
    Task<IEnumerable<CategoryDto>> GetAllAsync();

    // Get a single Category by ID
    Task<CategoryDto?> GetByIdAsync(string id);

    // Add a new Category
    Task<CategoryDto> AddAsync(CreateCategoryDto createCategoryDto);

    // Update an existing Category
    Task<CategoryDto> UpdateAsync(string id, UpdateCategoryDto updateCategoryDto);

    // Delete an Category by ID
    Task DeleteAsync(string id);
}