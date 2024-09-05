using AutoMapper;
using Todo.API.DTOs.Categories;
using Todo.API.DTOs.TodoItems;
using Todo.API.Models;

namespace Todo.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTodoItemDto, TodoItem>();
        CreateMap<UpdateTodoItemDto, TodoItem>();
        CreateMap<TodoItem, TodoItemDto>();
    }
}