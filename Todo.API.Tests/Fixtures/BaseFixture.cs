using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.API.Data;
using Todo.API.Mappings;
using Todo.API.Repositories;
using Todo.API.Repositories.Contracts;
using Todo.API.Services;
using Todo.API.Tests.Factories;

namespace Todo.API.Tests.Fixtures;

public class BaseFixture : IDisposable
{
    public ServiceProvider ServiceProvider { get; }
    public Random Random { get; }

    public BaseFixture()
    {
        var serviceCollection = new ServiceCollection();

        ConfigureServices(serviceCollection);

        Random = new Random();
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
        });

        #region Repositories
        services.AddScoped<TodoItemsRepository>();
        services.AddScoped<ITodoItemsRepository, TodoItemsRepository>();

        services.AddScoped<CategoriesRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        #endregion

        #region Services
        services.AddScoped<TodoItemsService>();
        services.AddScoped<CategoriesService>();
        #endregion

        #region Factories
        services.AddScoped<TodoItemsFactory>();
        services.AddScoped<CategoriesFactory>();
        #endregion

        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
    }

    public void Dispose()
    {
        var appDbContext = ServiceProvider.GetRequiredService<ApplicationDbContext>();
        appDbContext.Dispose();
    }

    public async Task CleanDatabase()
    {
        var appDbContext = ServiceProvider.GetRequiredService<ApplicationDbContext>();

        appDbContext.TodoItems.RemoveRange(appDbContext.TodoItems);
        appDbContext.Categories.RemoveRange(appDbContext.Categories);

        await appDbContext.SaveChangesAsync();
    }
}