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
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    private TodoItem Map(DomainTodoItem todo) => new()
    {
        Id = todo.Id,
        Description = todo.Description,
        CompleteBy = todo.CompleteBy,
        IsComplete = todo.IsComplete
    };

   
}
