using TodoApp.Core.Models;

namespace TodoApp.DataAccess;

public class DictionaryTodoRepository : ITodoRepository
{
    private Dictionary<int, TodoItem> _todoItems = new()
    {
        [1] = new()
        {
            Id = 1,
            Description = "Sort life out",
            CompleteBy = DateTime.Now
        },
        [2] = new()
        {
            Id = 2,
            Description = "Do washing up",
            CompleteBy = DateTime.Now.AddDays(-2),
            IsComplete = true
        },
        [3] = new()
        {
            Id = 3,
            Description = "Walk dog",
            CompleteBy = DateTime.Now.AddHours(1)
        }
    };
    private int _lastId = 3;
    public List<TodoItem> GetAll() => _todoItems.Values.ToList();
    
    public TodoItem? GetById(int id) => _todoItems.TryGetValue(id, out var todo) ? todo : null;
  
    public int? Create(TodoItem todo)
    {
        var todoItem = new TodoItem
        {
            Id = ++_lastId,
            Description = todo.Description,
            CompleteBy = todo.CompleteBy
        };

        _todoItems.Add(todoItem.Id, todoItem);
        return _lastId;
    }

    public bool Update(int id, TodoItem todo)
    {
        if (!_todoItems.TryGetValue(id, out var todoItem))
        {
            return false;
        }

        todoItem.Description = todo.Description;
        todoItem.IsComplete = todo.IsComplete;
        todoItem.CompleteBy = todo.CompleteBy;

        return true;
    }

    public bool Delete(int id)
    {
        if (!_todoItems.TryGetValue(id, out var todoItem))
        {
            return false;
        }

        _todoItems.Remove(id);
        return true;
    }
}
