using Api.Dtos;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public ActionResult<List<TaskShortDto>> GetAll()
    {
        var tasks = _taskService.GetAll();

        var result = tasks.Select(task => new TaskShortDto
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted,
            Deadline = task.Deadline
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public ActionResult<TaskFullDto> GetById(Guid id)
    {
        var task = _taskService.GetById(id);

        if (task is null)
        {
            return NotFound();
        }

        var result = new TaskFullDto
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted,
            Deadline = task.Deadline
        };

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<TaskFullDto> Create(CreateTaskDto dto)
    {
        var task = _taskService.Create(dto.Title, dto.Deadline);

        var result = new TaskFullDto
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted,
            Deadline = task.Deadline
        };

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public ActionResult<TaskFullDto> Update(Guid id, UpdateTaskDto dto)
    {
        var task = _taskService.Update(
            id,
            dto.Title,
            dto.Deadline,
            dto.IsCompleted
        );

        if (task is null)
        {
            return NotFound();
        }

        var result = new TaskFullDto
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted,
            Deadline = task.Deadline
        };

        return Ok(result);
    }

    [HttpPatch("{id:guid}/complete")]
    public ActionResult<TaskFullDto> Complete(Guid id)
    {
        var task = _taskService.Complete(id);

        if (task is null)
        {
            return NotFound();
        }

        var result = new TaskFullDto
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted,
            Deadline = task.Deadline
        };

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var isDeleted = _taskService.Delete(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}