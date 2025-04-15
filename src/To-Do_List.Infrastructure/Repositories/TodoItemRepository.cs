using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.Entities;
using To_Do_List.Core.DomainService.Repository;
using To_Do_List.Infrastructure.Persistence.Context;

namespace To_Do_List.Infrastructure.Persistence.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly DbSet<TodoItem> _todoItems;
    public TodoItemRepository(AppDBContext context)
    {
        _todoItems = context.Set<TodoItem>();
    }

    public async Task AddTodoItemAsync(TodoItem entity)
    {
         await _todoItems.AddAsync(entity);
    }

    public void DeleteTodoItem(TodoItem entity)
    {
        _todoItems.Remove(entity);
    }

    public async Task<IEnumerable<TodoItem>> GetAllTodoItemAsync()
    {
        return await _todoItems.ToListAsync();
    }

    public async Task<(IEnumerable<TodoItem>,int)> GetPagedTodoItemForUserAsync(int idUser, int page, int pageSize)
    {
        var totalItemsTask = _todoItems.CountAsync(x => x.UserId == idUser);

        var itemsTask = _todoItems
            .Where(x => x.UserId == idUser)
            .OrderBy(x => x.CreatedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalItems = await totalItemsTask;
        var items = await itemsTask;
        return (items, totalItems);
    }

    public async Task<TodoItem> GetTodoItemByIdAsync(int id)
    {
        return await _todoItems.FindAsync(id);
    }

    public void UpdateTodoItem(TodoItem entityToUpdate)
    {
        _todoItems.Update(entityToUpdate);
    }
}

