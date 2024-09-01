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

    public TodoItemDto MapTodoItemToDto(TodoItem todoItem)
    {
        return new TodoItemDto()
        {
            Id = todoItem.Id,
            Description = todoItem.Description,
            Title = todoItem.Title,
            DueDate = todoItem.DueDate,
            CreatedAt = todoItem.CreatedAt,
            Priority = todoItem.Priority,
            Status = todoItem.Status,
            UpdatedAt = todoItem.UpdatedAt
        };
    }
}
