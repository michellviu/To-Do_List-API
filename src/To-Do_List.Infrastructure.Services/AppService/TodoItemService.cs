using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.Entities;
using To_Do_List.Core.DomainService.Services;
using To_Do_List.Infrastructure.Persistence.UnitWork;

namespace To_Do_List.Infrastructure.Services.AppService;

internal class TodoItemService : ITodoItemService
{
    private readonly UnitWork unitWork;
    public TodoItemService(UnitWork unitWork)
    {
        this.unitWork = unitWork;
    }

    public async Task AddTodoItemAsync(TodoItem entity)
    {
        entity.CreatedDate = DateTime.Now;
        entity.LastUpdatedDate = DateTime.Now;
        await unitWork.TaskRepository.AddTodoItemAsync(entity);
        await unitWork.SaveAsync();
    }

    public async Task<OneOf<TodoItem, string>> DeleteTodoItemAsync(int id)
    {
        var entity = await unitWork.TaskRepository.GetTodoItemByIdAsync(id);
        if (entity == null)
        {
            return "TodoItem not found";
        }
        unitWork.TaskRepository.DeleteTodoItem(entity);
        await unitWork.SaveAsync();
        return entity;
    }

    public Task<IEnumerable<TodoItem>> GetAllTodoItemAsync()
    {
        return unitWork.TaskRepository.GetAllTodoItemAsync();
    }

    public Task<(IEnumerable<TodoItem>,int)> GetPagedTodoItemForUserAsync(int idUser, int page, int pageSize)
    {
        return unitWork.TaskRepository.GetPagedTodoItemForUserAsync(idUser, page, pageSize);
    }
    

    public Task<TodoItem> GetTodoItemByIdAsync(int id)
    {
        return unitWork.TaskRepository.GetTodoItemByIdAsync(id);
    }

    public async Task<OneOf<TodoItem,string>> UpdateTodoItemAsync(int id, TodoItem entity)
    {
        var entityToUpdate = await GetTodoItemByIdAsync(id);
        if (entityToUpdate == null)
        {
            return "TodoItem not found";
        }
        entityToUpdate.Id = id;
        entityToUpdate.Title = entity.Title;
        entityToUpdate.Description = entity.Description;
        entityToUpdate.Status = entity.Status;
        entityToUpdate.Difficulty = entity.Difficulty;
        entityToUpdate.LastUpdatedDate = DateTime.Now;

        unitWork.TaskRepository.UpdateTodoItem(entityToUpdate);
        await unitWork.SaveAsync();
        return entityToUpdate;

    }
}

