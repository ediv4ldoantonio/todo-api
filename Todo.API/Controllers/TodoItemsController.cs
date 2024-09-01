using Microsoft.AspNetCore.Mvc;
using Todo.API.DTOs;
using Todo.API.Services.Contracts;

namespace Todo.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoItemsService todoItemsService;

    public TodoItemsController(ITodoItemsService todoItemsService)
    {
        this.todoItemsService = todoItemsService;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<TodoItemDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTodoItems()
    {
        var todoItems = await todoItemsService.GetAllAsync();

        return Ok(todoItems);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        TodoItemDto? todoItem = await todoItemsService.GetByIdAsync(id);

        if (todoItem == null)
            return NotFound();

        return Ok(todoItem);
    }

    [HttpPost]
    [ProducesResponseType<TodoItemDto>(StatusCodes.Status201Created)]
    public async Task<IActionResult> SaveTodoItem(CreateTodoItemDto createTodoItemDto)
    {
        TodoItemDto todoItem = await todoItemsService.AddAsync(createTodoItemDto);

        return CreatedAtAction(nameof(SaveTodoItem), todoItem);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodoItem(string id)
    {
        try
        {
            await todoItemsService.DeleteAsync(id);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType<TodoItemDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTodoItem(string id, UpdateTodoItemDto updateTodoItemDto)
    {
        TodoItemDto? todoItem;

        try
        {
            todoItem = await todoItemsService.UpdateAsync(id, updateTodoItemDto);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return Ok(todoItem);
    }
}