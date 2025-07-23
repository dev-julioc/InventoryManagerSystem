namespace InventoryManagerSystem.Domain.Entities;

public sealed class Location
{
    public int Id { get; private set; }
    public string? Name { get; private set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();

    public Location(string name)
    {
        
    }
    
    public void Update(string name) 
        => Name = name;
}