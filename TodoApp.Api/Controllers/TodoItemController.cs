using System;
using System.Collections.Generic;
using System.Linq;
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
    
}
