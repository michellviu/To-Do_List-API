using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.Core.Domain.DTOs;
using To_Do_List.Core.DomainService.Services;
using To_Do_List.Infrastructure.Persistence.Mappers;
using System;
using OneOf;
using To_Do_List.Api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace To_Do_List.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITodoItemService _taskService;
    public TaskController(ITodoItemService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("tasks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTasks([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page < 1 || pageSize < 1)
        return BadRequest("Page and pageSize must be greater than 0.");

        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

        var (tasks,totalItems) = await _taskService.GetPagedTodoItemForUserAsync(userId,page, pageSize);
        var taskDtos = tasks.Select(TodoItemMapper.FromEntityToResponseDto).ToList();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var response = new PagedResponse
        {
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            Items = taskDtos
        };
        return Ok(response);
    }
    
    /// <summary>
    /// Obtiene una tarea especifica del usuario autenticado.
    /// </summary>
    /// <remarks>
    /// Este endpoint devuelve una tarea asociada al usuario autenticado. 
    /// remarks se usa para una descripcion mas detallada.
    /// </remarks>
    /// <param name="id">Representa el identificador unicos del item.</param>
    /// <returns>Un item.</returns>
    [HttpGet("task/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTask([FromRoute]int id)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
        var task = await _taskService.GetTodoItemByIdAsync(id);
        if (task == null)
            return NotFound("Entity no found.");
        if (task.UserId != userId)
            return Unauthorized();
        return Ok(TodoItemMapper.FromEntityToResponseDto(task));
    }


    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateTask([FromBody] TodoItemRequestDto taskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
        var task = TodoItemMapper.FromRequestDtoToEntity(taskDto, userId);
        await _taskService.AddTodoItemAsync(task);
        return CreatedAtAction(nameof(GetTasks), TodoItemMapper.FromEntityToResponseDto(task));
    }


    [HttpPut("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTask([FromRoute]int id, [FromBody] TodoItemRequestDto taskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

        var taskentity = await _taskService.GetTodoItemByIdAsync(id);
        if (taskentity == null)
            return NotFound("Entity no found.");
        if (taskentity.UserId != userId)
            return Unauthorized();
        var task = TodoItemMapper.FromRequestDtoToEntity(taskDto, userId);
        var result = await _taskService.UpdateTodoItemAsync(id, task);

        return result.Match<IActionResult>(
            task => Ok(TodoItemMapper.FromEntityToResponseDto(task)),
            error => NotFound("Entity no found.")
        );

    }


    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTask([FromRoute]int id)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

        var task = await _taskService.GetTodoItemByIdAsync(id);
        if (task == null)
            return NotFound("Entity no found.");
        if (task.UserId != userId)
            return Unauthorized();

        var result = await _taskService.DeleteTodoItemAsync(id);

        return result.Match<IActionResult>(
           task => NoContent(),
           error => NotFound("Entity no found.")
       );

    }
}


