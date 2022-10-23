namespace TodoApp.Core.Models;
public class TodoItem
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime? CompleteBy { get; set; }
    public DateTime? CompletedOn { get; set; }
    public bool IsComplete { get; set; }
}