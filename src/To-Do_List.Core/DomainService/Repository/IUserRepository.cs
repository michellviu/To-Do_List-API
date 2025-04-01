using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.Entities;

namespace To_Do_List.Core.DomainService.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
