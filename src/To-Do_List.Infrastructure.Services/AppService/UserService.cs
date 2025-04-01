using To_Do_List.Core.Domain.Entities;
using To_Do_List.Core.DomainService.Services;
using Task = System.Threading.Tasks.Task;
using To_Do_List.Infrastructure.Persistence.UnitWork;

namespace To_Do_List.Infrastructure.Services.AppService
{
    internal class UserService : IUserService
    {
        private readonly UnitWork unitWork;
        public UserService(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }
        public async Task<User> AddAsync(User entity)
        {
            await unitWork.UserRepository.AddAsync(entity);
            await unitWork.SaveAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }
            unitWork.UserRepository.Delete(entity);
            await unitWork.SaveAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await unitWork.UserRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await unitWork.UserRepository.GetByIdAsync(id);
        }

        public async Task<User> UpdateAsync(int id,User entity)
        {
            var entityToUpdate = await GetByIdAsync(id);
            if (entityToUpdate == null)
            {
                throw new Exception("Entity not found");
            }

            entityToUpdate.Email = entity.Email;
            entityToUpdate.UserName = entity.UserName;

            unitWork.UserRepository.Update(entityToUpdate);
            await unitWork.SaveAsync();
            return entityToUpdate;
        }
    }
}
