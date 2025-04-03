using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.Core.Domain.DTOs;
using To_Do_List.Core.DomainService.Services;
using To_Do_List.Infrastructure.Persistence.Mappers;

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
    public async Task<IActionResult> GetTasks()
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
        var tasks = await _taskService.GetAllTodoItemForUserAsync(userId);
        var taskDtos = tasks.Select(TodoItemMapper.ToDto).ToList();
        return Ok(taskDtos);
    }


    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateTask([FromBody] TodoItemDto taskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
        var task = TodoItemMapper.ToEntity(taskDto, userId);
        await _taskService.AddTodoItemAsync(task);
        return CreatedAtAction(nameof(GetTasks), TodoItemMapper.ToDto(task));
    }

    [HttpPut("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TodoItemDto taskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
        var taskentity = await _taskService.GetTodoItemByIdAsync(id);
          if (taskentity.UserId != userId)
            return Unauthorized();
        var task = TodoItemMapper.ToEntity(taskDto, userId);
        await _taskService.UpdateTodoItemAsync(id, task);
        task.Id = id;
        return Ok(TodoItemMapper.ToDto(task));
    }


    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
        var task = await _taskService.GetTodoItemByIdAsync(id);
        if (task.UserId != userId)
            return Unauthorized();
        await _taskService.DeleteTodoItemAsync(id);
        return NoContent();
    }

}


