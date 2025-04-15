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

    public static TodoItemResponseDto FromEntityToResponseDto(TodoItem task)
    {
        return new TodoItemResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            CreatedDate = task.CreatedDate,
            LastUpdatedDate = task.LastUpdatedDate,
            Difficulty = task.Difficulty
        };
    }

    public static TodoItem FromRequestDtoToEntity(TodoItemRequestDto taskDto, int userId)
    {

        var task = new TodoItem
        {
            Id = taskDto.Id,
            Title = taskDto.Title,
            Description = taskDto.Description,
            Status = taskDto.Status,
            Difficulty = taskDto.Difficulty,
            UserId = userId
        };
        return task; 
    }
}

