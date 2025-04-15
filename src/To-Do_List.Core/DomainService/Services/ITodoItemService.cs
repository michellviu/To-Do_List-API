using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoItem = To_Do_List.Core.Domain.Entities.TodoItem;


namespace To_Do_List.Core.DomainService.Services;

public interface ITodoItemService
{
    Task<IEnumerable<TodoItem>> GetAllTodoItemAsync();
    Task<(IEnumerable<TodoItem>,int)> GetPagedTodoItemForUserAsync(int idUser, int page, int pageSize);
    Task<TodoItem> GetTodoItemByIdAsync(int id);
    Task AddTodoItemAsync(TodoItem entity);
    Task<OneOf<TodoItem, string>> UpdateTodoItemAsync(int id, TodoItem entity);
    Task<OneOf<TodoItem, string>> DeleteTodoItemAsync(int id);

}

