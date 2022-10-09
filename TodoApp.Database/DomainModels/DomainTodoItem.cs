namespace TodoApp.DataAccess.DomainModels;
public class DomainTodoItem
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime? CompleteBy { get; set; }
    public bool IsComplete { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
