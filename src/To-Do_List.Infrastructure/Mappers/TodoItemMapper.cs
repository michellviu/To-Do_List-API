using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using To_Do_List.Core.Domain.DTOs;
using To_Do_List.Core.DomainService.Services;
using TodoItem = To_Do_List.Core.Domain.Entities.TodoItem;

namespace To_Do_List.Infrastructure.Persistence.Mappers
{
    public class TodoItemMapper
    {
        private ITodoItemService _taskService;
        public TodoItemMapper(ITodoItemService taskService)
        {
            _taskService = taskService;
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

        public static TodoItem ToEntity(TodoItemDto taskDto)
        {
            return new TodoItem
            {
                Id = taskDto.Id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                State = taskDto.State,
                CreatedDate = taskDto.CreatedDate,
                LastUpdatedDate = taskDto.LastUpdatedDate
            };
        }
    }
}
