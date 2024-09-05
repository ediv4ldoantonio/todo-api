using AutoMapper;
using Todo.API.DTOs.TodoItems;
using Todo.API.Models;
using Todo.API.Repositories.Contracts;
using Todo.API.Services.Contracts;

namespace Todo.API.Services;

public class TodoItemsService : ITodoItemsService
{
    private readonly ITodoItemsRepository todoItemsRepository;
    private readonly IMapper mapper;

    public TodoItemsService(ITodoItemsRepository todoItemsRepository, IMapper mapper)
    {
        this.todoItemsRepository = todoItemsRepository;
        this.mapper = mapper;
    }
    public async Task<TodoItemDto> AddAsync(CreateTodoItemDto createTodoItem)
    {
        TodoItem todoItem = mapper.Map<TodoItem>(createTodoItem);

        await todoItemsRepository.AddAsync(todoItem);

        return mapper.Map<TodoItemDto>(todoItem);
    }

    public async Task DeleteAsync(string id)
    {
        await todoItemsRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
    {
        IEnumerable<TodoItem> todoItems = await todoItemsRepository.GetAllAsync();

        return mapper.Map<IEnumerable<TodoItemDto>>(todoItems);
    }

    public async Task<TodoItemDto?> GetByIdAsync(string id)
    {
        TodoItem? todoItem = await todoItemsRepository.GetByIdAsync(id);

        if (todoItem == null)
            return null;

        return mapper.Map<TodoItemDto>(todoItem);
    }

    public async Task<TodoItemDto> UpdateAsync(string id, UpdateTodoItemDto updateTodoItemDto)
    {
        TodoItem todoItem = await todoItemsRepository.GetByIdAsync(id)
                ?? throw new Exception("TodoItem Not Found");

        mapper.Map(updateTodoItemDto, todoItem);

        todoItem.UpdatedAt = DateTime.Now;

        await todoItemsRepository.UpdateAsync(todoItem);

        return mapper.Map<TodoItemDto>(todoItem);
    }
}