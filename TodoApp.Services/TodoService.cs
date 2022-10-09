using TodoApp.Core;
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

    public TodoItem? GetById(int id) => _todoRepository.GetById(id);

    public CreateTodoItemResult Create(string description, DateTime? completeByDate)
    {
        var todo = new TodoItem
        {
            Description = description,
            CompleteBy = completeByDate
        };

        var errors = Validate(todo);

        if (errors.Any())
        {
            return new CreateTodoItemResult(errors);
        }

        var id = _todoRepository.Create(todo);

        if (id is null)
        {
            errors.Add("Could not create todo item");
            return new CreateTodoItemResult(errors);
        }

        return new CreateTodoItemResult(errors, id);
    }


    public ValidationResult Update(int id, TodoItem todo)
    {
        var errors = Validate(todo);

        if (errors.Any())
        {
            return new ValidationResult(errors);
        }

        var isUpdated = _todoRepository.Update(id, todo);

        if (!isUpdated)
        {
            return new ValidationResult(new() { $"Could not update {todo.Description}"});
        }

        return new();
    }

    public ValidationResult Delete(int id)
    {
        if (!_todoRepository.Delete(id))
        {
            return new ValidationResult(new() { $"Could not delete item with id {id}" });
        }

        return new();
    }

    private static List<string> Validate(TodoItem todo)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(todo.Description))
        {
            errors.Add("Description is required");
        }

        if (todo.Description?.Length > 256)
        {
            errors.Add("Description cannot be longer than 256 characters");
        }

        return errors;
    }
}
