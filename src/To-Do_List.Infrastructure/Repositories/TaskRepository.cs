using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.Entities;
using To_Do_List.Core.DomainService.Repository;
using To_Do_List.Infrastructure.Persistence.Context;
using Task = To_Do_List.Core.Domain.Entities.Task;

namespace To_Do_List.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        private readonly DbSet<Task> _task;
        public TaskRepository(AppDBContext context) : base(context)
        {
            _task = context.Set<Task>();
        }
    }
}
