using Todo.API.DTOs;
using Todo.API.Enums;
using Todo.API.Models;

namespace Todo.API.Tests.Factories;

public class TodoItemsFactory
{
    private readonly Random random;

    public TodoItemsFactory()
    {
        random = new Random();
    }

    public TodoItem GetTodoItem()
    {
        return new()
        {
            Title = $"Test-{random.Next()}",
            Description = $"desc-{random.Next()}",
            DueDate = DateTime.Now.AddDays(random.Next(1, 7)),
            Priority = Priority.High,
            Status = Status.Pending
        };
    }
}
