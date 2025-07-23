namespace InventoryManagerSystem.Domain.Entities;

public sealed class Product
{
    public int Id { get; private set; }
    public string? Name { get; private set; }
    public string? SerialNumber { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public string? Description { get; private set; }
    public string? Base64Image { get; private set; }
    public DateTime DateAdded { get; private set; }
    
    public int CategoryId { get; set; }
    public int LocationId { get; set; }
    
    public Category? Category { get; set; }
    public Location? Location { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();


    public Product(string name, string serialNumber, decimal price, int quantity,
        string description, string base64Image, int categoryId, int locationId)
    {
        DateAdded = DateTime.Now;
    }

    public void Update(string name, string serialNumber, decimal price, int quantity,
        string description, string base64Image, DateTime dateAdded, int categoryId, int locationId)
    {
        Name = name;
        SerialNumber = serialNumber;
        Price = price;
        Quantity = quantity;
        Description = description;
        Base64Image = base64Image;
        DateAdded = dateAdded;
        CategoryId = categoryId;
        LocationId = locationId;
    }
}