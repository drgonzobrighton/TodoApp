using System.Collections.Generic;
using TodoApp.Core.Models;

namespace TodoApp.DataAccess;
public interface ITodoRepository
{
    List<TodoItem> GetAll();
    TodoItem GetById(int id);
    int? Create(TodoItem todo);
    bool Update(int id, TodoItem todo);
    bool Delete(int id);
}