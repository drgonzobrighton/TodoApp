using System.Collections.Generic;

namespace TodoApp.Services;
public class ValidationResult
{
    private List<string> _errors { get; } = new();
    public bool Success => _errors.Count == 0;
    public int? ItemId { get; set; }

    public ValidationResult()
    {
    }

    public ValidationResult(int? itemId) => ItemId = itemId;
    
    public void AddError(string error)
    {
        _errors.Add(error);
    }

    public List<string> GetErrors() => _errors;
}