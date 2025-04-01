using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using To_Do_List.Core.Domain.DTOs;
using To_Do_List.Core.DomainService.Services;
using Task = To_Do_List.Core.Domain.Entities.Task;

namespace To_Do_List.Infrastructure.Persistence.Mappers
{
    public class TaskMapper
    {
        private ITaskService _taskService;
        public TaskMapper(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public static TaskDto ToDto(Task task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                State = task.State,
                CreatedDate = task.CreatedDate,
                LastUpdatedDate = task.LastUpdatedDate
            };
        }

        public static Task ToEntity(TaskDto taskDto)
        {
            return new Task
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
