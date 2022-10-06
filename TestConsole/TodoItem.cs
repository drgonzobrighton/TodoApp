using System;

namespace TodoApp;
public class TodoItem
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime? CompleteBy { get; set; }
    public bool IsComplete { get; set; }
}
