using Microsoft.AspNetCore.Mvc;
using Todo.API.DTOs.Categories;
using Todo.API.Services.Contracts;

namespace Todo.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
    {
        this.categoriesService = categoriesService;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<CategoryDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await categoriesService.GetAllAsync();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<CategoryDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        CategoryDto? category = await categoriesService.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType<CategoryDto>(StatusCodes.Status201Created)]
    public async Task<IActionResult> SaveTodoItem(CreateCategoryDto createCategoryDto)
    {
        CategoryDto category = await categoriesService.AddAsync(createCategoryDto);

        return CreatedAtAction(nameof(SaveTodoItem), category);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        try
        {
            await categoriesService.DeleteAsync(id);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType<CategoryDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTodoItem(string id, UpdateCategoryDto updateCategoryDto)
    {
        CategoryDto? category;

        try
        {
            category = await categoriesService.UpdateAsync(id, updateCategoryDto);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return Ok(category);
    }
}