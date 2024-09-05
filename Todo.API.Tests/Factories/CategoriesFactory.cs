using Todo.API.Models;

namespace Todo.API.Tests.Factories;

public class CategoriesFactory
{
    private readonly Random random;

    public CategoriesFactory()
    {
        random = new Random();
    }

    public Category GetCategory()
    {
        return new Category()
        {
            Name = $"Cat-{random.Next()}",
            Description = $"desc-{random.Next()}"
        };
    }
}