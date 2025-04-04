using System.Threading.Tasks;
using To_Do_List.Core.Domain.DTOs;
using To_Do_List.Core.Domain.Entities;
using To_Do_List.Core.DomainService.Services;

namespace To_Do_List.Infrastructure.Persistence.Mappers;

public class TodoItemMapper
{
    private ITodoItemService _todoitemService;
    public TodoItemMapper(ITodoItemService todoitemService)
    {
        _todoitemService = todoitemService;
    }

    public static TodoItemDto ToDto(TodoItem task)
    {
        return new TodoItemDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            State = task.State,
            CreatedDate = task.CreatedDate,
            LastUpdatedDate = task.LastUpdatedDate
        };
    }

    public static TodoItem ToEntity(TodoItemDto taskDto, int userId)
    {
        var task = new TodoItem
        {
            Id = taskDto.Id,
            Title = taskDto.Title,
            Description = taskDto.Description,
            State = taskDto.State,
            CreatedDate = taskDto.CreatedDate,
            LastUpdatedDate = taskDto.LastUpdatedDate,
            UserId = userId
        };
        return task;
    }
}

