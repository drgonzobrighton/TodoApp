using System;
using System.Collections.Generic;
using System.Linq;
using TodoApp.Core.Models;
using TodoApp.DataAccess.DomainModels;

namespace TodoApp.DataAccess;
public class EfTodoRepository : ITodoRepository
{
    private readonly TodoContext _context;

    public EfTodoRepository(TodoContext context)
    {
        _context = context;
    }

    public List<TodoItem> GetAll() => _context.TodoItems.Select(Map).ToList();

    public TodoItem? GetById(int id)
    { 
        var domainItem = _context.TodoItems.FirstOrDefault(x => x.Id == id);

        return domainItem is null ? null : Map(domainItem);
    }
    
    public int? Create(TodoItem todo)
    {
        var domain = new DomainTodoItem()
        {
            Description = todo.Description,
            CompleteBy = todo.CompleteBy,
            IsComplete = todo.IsComplete,
            CreatedDate = DateTime.UtcNow
        };

        _context.TodoItems.Add(domain); 
        _context.SaveChanges();
        return domain.Id;
    }

    public bool Update(int id, TodoItem todo)
    {
        var domainTodoItem = _context.TodoItems.FirstOrDefault(x => x.Id == id);

        if (domainTodoItem is null)
            return false;

        domainTodoItem.Description = todo.Description;
        domainTodoItem.CompleteBy = todo.CompleteBy;
        domainTodoItem.CompletedOn = todo.CompletedOn;
        domainTodoItem.IsComplete = todo.IsComplete;
        domainTodoItem.ModifiedDate = DateTime.UtcNow;

       return _context.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        var domainTodoItem = _context.TodoItems.FirstOrDefault(x => x.Id == id);

        if (domainTodoItem is null)
            return false;

        _context.TodoItems.Remove(domainTodoItem);

        return _context.SaveChanges() > 0;
    }

    private static TodoItem Map(DomainTodoItem todo) => new()
    {
        Id = todo.Id,
        Description = todo.Description,
        CompleteBy = todo.CompleteBy,
        CompletedOn = todo.CompletedOn,
        IsComplete = todo.IsComplete
    };
}