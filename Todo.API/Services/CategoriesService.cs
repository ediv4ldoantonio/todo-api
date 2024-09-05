using AutoMapper;
using Todo.API.DTOs;
using Todo.API.DTOs.Categories;
using Todo.API.Models;
using Todo.API.Repositories;
using Todo.API.Repositories.Contracts;
using Todo.API.Services.Contracts;

namespace Todo.API.Services;

public class CategoriesService : ICategoriesService
{
    private readonly IMapper mapper;
    private readonly ICategoriesRepository categoriesRepository;

    public CategoriesService(IMapper mapper, ICategoriesRepository categoriesRepository)
    {
        this.mapper = mapper;
        this.categoriesRepository = categoriesRepository;
    }

    public async Task<CategoryDto> AddAsync(CreateCategoryDto createCategoryDto)
    {
        Category category = mapper.Map<Category>(createCategoryDto);

        await categoriesRepository.AddAsync(category);

        return mapper.Map<CategoryDto>(category);
    }

    public async Task DeleteAsync(string id)
    {
        await categoriesRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await categoriesRepository.GetAllAsync();

        return mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetByIdAsync(string id)
    {
        Category? category = await categoriesRepository.GetByIdAsync(id);

        return mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateAsync(string id, UpdateCategoryDto updateCategoryDto)
    {
        Category category = await categoriesRepository.GetByIdAsync(id)
                ?? throw new Exception("Category Not Found");

        mapper.Map(updateCategoryDto, category);

        category.UpdatedAt = DateTime.Now;

        await categoriesRepository.UpdateAsync(category);

        return mapper.Map<CategoryDto>(category);
    }
}