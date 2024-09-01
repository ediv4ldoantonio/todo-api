using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.API.Data;
using Todo.API.Repositories;

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
            options.UseInMemoryDatabase("todo-api");
        });

        #region Repositories
        services.AddScoped<TodoItemsRepository>();
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

        await appDbContext.Database.EnsureDeletedAsync();
    }
}