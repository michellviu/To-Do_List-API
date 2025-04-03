using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoItem = To_Do_List.Core.Domain.Entities.TodoItem;


namespace To_Do_List.Core.DomainService.Services;

public interface ITodoItemService
{
    Task<IEnumerable<TodoItem>> GetAllTodoItemAsync();
    Task<IEnumerable<TodoItem>> GetAllTodoItemForUserAsync(int idUser);
    Task<TodoItem> GetTodoItemByIdAsync(int id);
    Task AddTodoItemAsync(TodoItem entity);
    Task UpdateTodoItemAsync(int id, TodoItem entity);
    Task DeleteTodoItemAsync(int id);

}

