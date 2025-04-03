using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.Core.Domain.DTOs;
using To_Do_List.Core.DomainService.Services;
using To_Do_List.Infrastructure.Persistence.Mappers;
using System;
using OneOf;

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
        return Ok(TodoItemMapper.ToDto(task));
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
    public async Task<IActionResult> UpdateTask([FromRoute]int id, [FromBody] TodoItemDto taskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

        var taskentity = await _taskService.GetTodoItemByIdAsync(id);
        if (taskentity == null)
            return NotFound("Entity no found.");
        if (taskentity.UserId != userId)
            return Unauthorized();
        var task = TodoItemMapper.ToEntity(taskDto, userId);
        var result = await _taskService.UpdateTodoItemAsync(id, task);

        return result.Match<IActionResult>(
            task => Ok(TodoItemMapper.ToDto(task)),
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


