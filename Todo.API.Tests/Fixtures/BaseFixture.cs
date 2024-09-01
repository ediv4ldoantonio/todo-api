using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.API.Data;
using Todo.API.Repositories;
using Todo.API.Repositories.Contracts;
using Todo.API.Services;

namespace Todo.API.Tests.Fixtures;

public class BaseFixture : IDisposable
{
    public ServiceProvider ServiceProvider { get; }

    public BaseFixture()
    {
        var serviceCollection = new ServiceCollection();

        ConfigureServices(serviceCollection);

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
        #endregion

        #region Services
        services.AddScoped<TodoItemsService>();
        #endregion
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

        await appDbContext.SaveChangesAsync();
    }
}