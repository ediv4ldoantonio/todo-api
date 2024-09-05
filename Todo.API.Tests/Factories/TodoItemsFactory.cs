using Todo.API.Enums;
using Todo.API.Models;

namespace Todo.API.Tests.Factories;

public class TodoItemsFactory
{
    private readonly Random random;
    private readonly CategoriesFactory categoriesFactory;

    public TodoItemsFactory(CategoriesFactory categoriesFactory)
    {
        random = new Random();
        this.categoriesFactory = categoriesFactory;
    }

    public TodoItem GetTodoItem()
    {
        return new()
        {
            Title = $"Test-{random.Next()}",
            Description = $"desc-{random.Next()}",
            DueDate = DateTime.Now.AddDays(random.Next(1, 7)),
            Priority = Priority.High,
            Status = Status.Pending,
            Category = categoriesFactory.GetCategory()
        };
    }

    public TodoItem GetTodoItem(Category category)
    {
        return new()
        {
            Title = $"Test-{random.Next()}",
            Description = $"desc-{random.Next()}",
            DueDate = DateTime.Now.AddDays(random.Next(1, 7)),
            Priority = Priority.High,
            Status = Status.Pending,
            Category = category
        };
    }
}
