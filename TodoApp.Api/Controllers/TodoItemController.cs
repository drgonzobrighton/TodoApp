using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Models;
using TodoApp.Services;

namespace TodoApp.Api.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoItemController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoItemController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public IEnumerable<TodoItem> Get() => _todoService.GetAll();

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var item = _todoService.GetById(id);

        if (item is null)
        {
            return new NotFoundResult();
        }

        return new JsonResult(item);
    }

    [HttpPost("create")]
    public ValidationResult Create(string description, DateTime? completeBy) => _todoService.Create(description, completeBy);

    [HttpPost("update/{id:int}")]
    public ValidationResult Update(int id, [FromBody] TodoItem item) => _todoService.Update(id, item);

    [HttpDelete]
    public ValidationResult Delete(int id) => _todoService.Delete(id);
   

    [HttpPost("markComplete/{id:int}")]
    public IActionResult MarkComplete(int id)
    {
        var item = _todoService.GetById(id);

        if (item is null)
        {
            return new NotFoundResult();
        }

        return new JsonResult(_todoService.Update(id, new()
        {
            Description = item.Description,
            CompleteBy = item.CompleteBy,
            CompletedOn = DateTime.Now,
            IsComplete = true
        }));
    }

  

}
