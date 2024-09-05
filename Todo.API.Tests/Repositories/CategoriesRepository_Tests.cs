using Microsoft.Extensions.DependencyInjection;
using Todo.API.Data;
using Todo.API.Models;
using Todo.API.Repositories;
using Todo.API.Tests.Factories;
using Todo.API.Tests.Fixtures;

namespace Todo.API.Tests.Repositories;

public class CategoriesRepository_Tests : IClassFixture<BaseFixture>
{
    private readonly CategoriesRepository categoriesRepository;
    private readonly CategoriesFactory categoriesFactory;
    private readonly ApplicationDbContext appDbContext;
    private readonly BaseFixture baseFixture;

    public CategoriesRepository_Tests(BaseFixture baseFixture)
    {
        this.baseFixture = baseFixture;

        this.categoriesRepository = baseFixture.ServiceProvider.GetRequiredService<CategoriesRepository>();
        this.categoriesFactory = baseFixture.ServiceProvider.GetRequiredService<CategoriesFactory>();
        this.appDbContext = baseFixture.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    [Fact]
    public async Task Test_Should_Create_Category()
    {
        await baseFixture.CleanDatabase();

        Category category = categoriesFactory.GetCategory();

        await categoriesRepository.AddAsync(category);

        List<Category> categories = [.. appDbContext.Categories];

        Assert.NotEmpty(categories);
        Assert.Single(categories);
        Assert.Equal(category.Id, categories[0].Id);
        Assert.Equal(category.Name, categories[0].Name);
        Assert.Equal(category.Description, categories[0].Description);
    }

    [Fact]
    public async Task Test_Should_Delete_Category()
    {
        await baseFixture.CleanDatabase();

        Category category = categoriesFactory.GetCategory();

        appDbContext.Categories.Add(category);

        await appDbContext.SaveChangesAsync();

        List<Category> categories = [.. appDbContext.Categories];

        Assert.NotEmpty(categories);

        await categoriesRepository.DeleteAsync(category.Id);

        categories = [.. appDbContext.Categories];

        Assert.Empty(categories);
    }

    [Fact]
    public async Task Test_Should_Get_All_Categories()
    {
        await baseFixture.CleanDatabase();

        for (int i = 0; i < 3; i++)
            appDbContext.Categories.Add(categoriesFactory.GetCategory());

        await appDbContext.SaveChangesAsync();

        IEnumerable<Category> categories = await categoriesRepository.GetAllAsync();
        Assert.NotEmpty(categories);
        Assert.Equal(3, categories.Count());
    }

    [Fact]
    public async Task Test_Should_Get_Category_By_Id()
    {
        await baseFixture.CleanDatabase();

        Category category = categoriesFactory.GetCategory();

        appDbContext.Categories.Add(category);

        await appDbContext.SaveChangesAsync();

        Category? retrievedCategory = await categoriesRepository.GetByIdAsync(category.Id);

        Assert.NotNull(retrievedCategory);
        Assert.Equal(category.Id, retrievedCategory.Id);
        Assert.Equal(category.Name, retrievedCategory.Name);
        Assert.Equal(category.Description, retrievedCategory.Description);
    }

    [Fact]
    public async Task Test_Should_Update_Category()
    {
        await baseFixture.CleanDatabase();

        Category category = categoriesFactory.GetCategory();

        appDbContext.Categories.Add(category);

        await appDbContext.SaveChangesAsync();

        category.Name = "changed name";

        await categoriesRepository.UpdateAsync(category);

        Category updatedCategory = appDbContext.Categories
                .Where(item => item.Id == category.Id)
                .FirstOrDefault()!;

        Assert.Equal(category.Id, category.Id);
        Assert.Equal("changed name", updatedCategory.Name);
    }
}