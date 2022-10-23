using System.Collections.Generic;

namespace TodoApp.Services;
public class ValidationResult
{
    private readonly List<string> _errors = new();

    public bool Success => _errors.Count == 0;
    public IReadOnlyCollection<string> Errors => _errors.Count > 0 ? _errors : null;
    public int? ItemId { get; set; }

    public ValidationResult()
    {
    }

    public ValidationResult(int? itemId) => ItemId = itemId;
    
    public void AddError(string error)
    {
        _errors.Add(error);
    }

}