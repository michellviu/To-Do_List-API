using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.Core.DomainService.UnitofWork;

public interface IUnitWork : IDisposable
{
    Task SaveAsync();
}

