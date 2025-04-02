using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.Entities;
using To_Do_List.Core.DomainService.Services;
using To_Do_List.Infrastructure.Persistence.UnitWork;

namespace To_Do_List.Infrastructure.Services.AppService
{
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

        public async Task DeleteTodoItemAsync(int id)
        {
            var entity = await unitWork.TaskRepository.GetTodoItemByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }
            unitWork.TaskRepository.DeleteTodoItem(entity);
            await unitWork.SaveAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodoItemAsync()
        {
           return await unitWork.TaskRepository.GetAllTodoItemAsync();
         
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodoItemForUserAsync(int idUser)
        {
            return await unitWork.TaskRepository.GetAllTodoItemForUserAsync(idUser); 
        }

        public async Task<TodoItem> GetTodoItemByIdAsync(int id)
        {
            return await unitWork.TaskRepository.GetTodoItemByIdAsync(id);
        }

        public async Task UpdateTodoItemAsync(int id,TodoItem entity)
        {
            var entityToUpdate = await GetTodoItemByIdAsync(id);
            if (entityToUpdate == null)
            {
                throw new Exception("Entity not found");
            }
            entityToUpdate.Id = id;
            entityToUpdate.Title = entity.Title;
            entityToUpdate.Description = entity.Description;
            entityToUpdate.State = entity.State;
            entityToUpdate.LastUpdatedDate = DateTime.Now;

            unitWork.TaskRepository.UpdateTodoItem(entityToUpdate);
            await unitWork.SaveAsync();
   
        }   
    }
}
