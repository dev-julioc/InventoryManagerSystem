namespace InventoryManagerSystem.WEB.Services.Results;

public class ResultService
{
    public bool IsSuccess { get; set; } = true;
    public string? Message { get; set; }
    public ICollection<ErrorValidation>? Errors { get; set; }
}

public class ResultService<T> : ResultService
{
    public T? Data { get; set; }
}
public class ErrorValidation
{
    public string Field { get; set; }
    public string Message { get; set; }
}