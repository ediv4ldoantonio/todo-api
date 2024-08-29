using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.API.DataContext;
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
        services.AddScoped<TodoItemRepository>();
        #endregion
    }

    public void Dispose()
    {
        var applicationDbContext = ServiceProvider.GetRequiredService<ApplicationDbContext>();
        applicationDbContext.Dispose();
    }
}