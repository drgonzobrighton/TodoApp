namespace TodoApp.Core.Models;
public class ValidationResult
{
    public List<string> Errors = new();
    public bool Success => Errors.Count == 0;

    public ValidationResult()
    {
    }

    public ValidationResult(List<string> errors)
    {
        Errors = errors;
    }

}

public class CreateTodoItemResult : ValidationResult
{
    public int? ItemId { get; set; }

    public CreateTodoItemResult(List<string> errors, int? itemId = null) : base(errors)
    {
        ItemId = itemId;
    }
}
