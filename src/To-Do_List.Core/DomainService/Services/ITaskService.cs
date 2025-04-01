using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task = To_Do_List.Core.Domain.Entities.Task;


namespace To_Do_List.Core.DomainService.Services
{
    public interface ITaskService : IGenericService<Task>
    {
    }
}
