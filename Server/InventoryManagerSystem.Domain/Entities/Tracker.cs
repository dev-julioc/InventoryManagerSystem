namespace InventoryManagerSystem.Domain.Entities;

public class Tracker
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool OperationState { get; set; }
    public string? UserId { get; set; }

    private Tracker()
    { }

    public Tracker(DateTime date, string title, string description, bool operationState, string userId)
    {
        Date = date;
        Title = title;
        Description = description;
        OperationState = operationState;
        UserId = userId;
    }
}