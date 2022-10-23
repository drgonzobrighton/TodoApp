using System;
using System.Collections.Generic;
using TodoApp.Core.Models;

namespace TodoApp.Services;
public interface ITodoService
{
    List<TodoItem> GetAll();
    TodoItem GetById(int id);
    ValidationResult Create(string description, DateTime? completeByDate);
    ValidationResult Update(int id, TodoItem todo);
    ValidationResult Delete(int id);
}