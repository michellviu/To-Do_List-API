using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.DomainService.Services;
using To_Do_List.Infrastructure.Persistence.UnitWork;

namespace To_Do_List.Infrastructure.Services.AppService
{
    internal class TaskService : ITaskService
    {
        private readonly UnitWork unitWork;
        public TaskService(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        public async Task<Core.Domain.Entities.Task> AddAsync(Core.Domain.Entities.Task entity)
        {
           entity.CreatedDate = DateTime.Now;
           entity.LastUpdatedDate = DateTime.Now;
           await unitWork.TaskRepository.AddAsync(entity);
           await unitWork.SaveAsync();
           return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await unitWork.TaskRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }
            unitWork.TaskRepository.Delete(entity);
            await unitWork.SaveAsync();
        }

        public async Task<IEnumerable<Core.Domain.Entities.Task>> GetAllAsync()
        {
           return await unitWork.TaskRepository.GetAllAsync();
        }

        public async Task<Core.Domain.Entities.Task> GetByIdAsync(int id)
        {
            return await unitWork.TaskRepository.GetByIdAsync(id);
        }

        public async Task<Core.Domain.Entities.Task> UpdateAsync(int id,Core.Domain.Entities.Task entity)
        {
            var entityToUpdate = await GetByIdAsync(id);
            if (entityToUpdate == null)
            {
                throw new Exception("Entity not found");
            }

            entityToUpdate.Title = entity.Title;
            entityToUpdate.Description = entity.Description;
            entityToUpdate.State = entity.State;
            entityToUpdate.LastUpdatedDate = DateTime.Now;

            unitWork.TaskRepository.Update(entityToUpdate);
            await unitWork.SaveAsync();
            return entityToUpdate;
        }
    }
}
