using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.API.Data;
using Todo.API.DTOs.Categories;
using Todo.API.Models;
using Todo.API.Services;
using Todo.API.Tests.Factories;
using Todo.API.Tests.Fixtures;

namespace Todo.API.Tests.Services;

public class CategoriesService_Tests : IClassFixture<BaseFixture>
{
    private readonly CategoriesService categoriesService;
    private readonly CategoriesFactory categoriesFactory;
    private readonly ApplicationDbContext appDbContext;
    private readonly BaseFixture baseFixture;

    public CategoriesService_Tests(BaseFixture baseFixture)
    {
        categoriesFactory = baseFixture.ServiceProvider.GetRequiredService<CategoriesFactory>();
        categoriesService = baseFixture.ServiceProvider.GetRequiredService<CategoriesService>();
        appDbContext = baseFixture.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        this.baseFixture = baseFixture;
    }

    [Fact]
    public async Task Test_Should_Create_Category()
    {
        await baseFixture.CleanDatabase();

        CategoryDto categoryDto = await categoriesService.AddAsync(new CreateCategoryDto()
        {
            Name = "Cat",
            Description = "desc"
        });

        var categories = await appDbContext.Categories.ToListAsync();

        Assert.Single(categories);
        Assert.NotNull(categoryDto);
        Assert.NotNull(categoryDto.Id);
        Assert.Equal(categoryDto.Name, categories[0].Name);
        Assert.Equal(categoryDto.Description, categories[0].Description);
    }

    [Fact]
    public async Task Test_Should_Get_All_Categories()
    {
        await baseFixture.CleanDatabase();

        List<Category> categories = [];

        for (int i = 0; i < 3; i++)
            categories.Add(categoriesFactory.GetCategory());

        appDbContext.Categories.AddRange(categories);

        await appDbContext.SaveChangesAsync();

        IEnumerable<CategoryDto> retrievedTodoItems = await categoriesService.GetAllAsync();

        Assert.NotEmpty(retrievedTodoItems);
        Assert.Equal(3, retrievedTodoItems.Count());
    }

    [Fact]
    public async Task Test_Should_Get_Category_By_Id()
    {
        await baseFixture.CleanDatabase();

        Category category = categoriesFactory.GetCategory();

        appDbContext.Categories.Add(category);

        await appDbContext.SaveChangesAsync();

        CategoryDto? retrievedCategory = await categoriesService.GetByIdAsync(category.Id);

        Assert.NotNull(retrievedCategory);
        Assert.Equal(category.Id, retrievedCategory.Id);
        Assert.Equal(category.Name, retrievedCategory.Name);
        Assert.Equal(category.Description, retrievedCategory.Description);
    }

    [Fact]
    public async Task Test_Should_Delete_Category()
    {
        await baseFixture.CleanDatabase();

        Category category = categoriesFactory.GetCategory();

        appDbContext.Categories.Add(category);

        await appDbContext.SaveChangesAsync();

        List<Category> categories = await appDbContext.Categories.ToListAsync();

        Assert.NotEmpty(categories);

        await categoriesService.DeleteAsync(category.Id);

        categories = await appDbContext.Categories.ToListAsync();

        Assert.Empty(categories);
    }

    [Fact]
    public async Task Test_Should_Update_Category()
    {
        await baseFixture.CleanDatabase();

        Category category = categoriesFactory.GetCategory();

        appDbContext.Categories.Add(category);
        await appDbContext.SaveChangesAsync();

        await categoriesService.UpdateAsync(category.Id, new UpdateCategoryDto()
        {
            Name = "updated",
            Description = "updated"
        });

        var categories = appDbContext.Categories.ToList();

        Assert.Equal("updated", categories[0].Name);
        Assert.Equal("updated", categories[0].Description);
    }
}