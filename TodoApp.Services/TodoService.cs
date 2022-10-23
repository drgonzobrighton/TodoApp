using System;
using System.Collections.Generic;
using TodoApp.Core.Models;
using TodoApp.DataAccess;

namespace TodoApp.Services;
public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public List<TodoItem> GetAll() => _todoRepository.GetAll();

    public TodoItem GetById(int id) => _todoRepository.GetById(id);

    public ValidationResult Create(string description, DateTime? completeByDate)
    {
        var todo = new TodoItem
        {
            Description = description,
            CompleteBy = completeByDate
        };

        var result = Validate(todo);

        if (!result.Success)
            return result;

        var id = _todoRepository.Create(todo);

        if (id is not null)
        {
            result.ItemId = id;
            return result;
        }

        result.AddError("Could not create todo item");
        return result;
    }


    public ValidationResult Update(int id, TodoItem todo)
    {
        var result = Validate(todo, id);

        if (!result.Success)
            return result;

        var isUpdated = _todoRepository.Update(id, todo);

        if (!isUpdated)
            result.AddError($"Could not update {todo.Description}");

        return result;
    }

    public ValidationResult Delete(int id)
    {
        var result = new ValidationResult(id);

        if (!_todoRepository.Delete(id))
            result.AddError($"Could not delete item with id {id}");

        return result;
    }

    private static ValidationResult Validate(TodoItem todo, int? itemId = null)
    {
        var result = new ValidationResult(itemId);

        if (string.IsNullOrEmpty(todo.Description))
        {
            result.AddError("Description is required");
        }

        if (todo.Description?.Length > 256)
        {
            result.AddError("Description cannot be longer than 256 characters");
        }

        return result;
    }
}
