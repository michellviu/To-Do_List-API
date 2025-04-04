using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.DomainService.Repository;
using To_Do_List.Core.DomainService.UnitofWork;
using To_Do_List.Infrastructure.Persistence.Context;

namespace To_Do_List.Infrastructure.Persistence.UnitWork;

public class UnitWork : IUnitWork, IDisposable
{
    private readonly AppDBContext _context;
    public ITodoItemRepository TaskRepository { get; set; }

    public UnitWork(AppDBContext context, ITodoItemRepository taskRepository)
    {
        _context = context;
        TaskRepository = taskRepository;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}

