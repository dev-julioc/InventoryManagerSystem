using InventoryManagerSystem.Domain.Validator;

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
        Validation(name, serialNumber, price, quantity, description, base64Image, categoryId, locationId);
        DateAdded = DateTime.Now;
    }

    public void Update(string name, string serialNumber, decimal price, int quantity,
        string description, string base64Image, DateTime dateAdded, int categoryId, int locationId)
    {
        Validation(name, serialNumber, price, quantity, description, base64Image, categoryId, locationId);
        DateAdded = dateAdded;
    }

    private void Validation(string name, string serialNumber, decimal price, int quantity, string description, string base64Image, int categoryId, int locationId)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), ValidationMessageBuilder.RequiredField("name"));
        DomainValidationException.When(name.Length is < 3 or > 150, ValidationMessageBuilder.RangeField("name", 3, 150));
        Name = name;

        DomainValidationException.When(string.IsNullOrEmpty(serialNumber), ValidationMessageBuilder.RequiredField("serial number"));
        DomainValidationException.When(serialNumber.Length is < 3 or > 250, ValidationMessageBuilder.RangeField("serialNumber", 3, 250));
        SerialNumber = serialNumber;

        DomainValidationException.When(price <= 0, ValidationMessageBuilder.GreaterThanZero("price"));
        Price = price;

        DomainValidationException.When(quantity < 0, ValidationMessageBuilder.Negative("quantity"));
        Quantity = quantity;

        DomainValidationException.When(string.IsNullOrEmpty(description), ValidationMessageBuilder.RequiredField("description"));
        DomainValidationException.When(description.Length > 250, ValidationMessageBuilder.MaxLengthField("description", 250));
        Description = description;
        
        DomainValidationException.When(string.IsNullOrEmpty(base64Image), ValidationMessageBuilder.RequiredField("base64 image"));
        DomainValidationException.When(description.Length > 500, ValidationMessageBuilder.MaxLengthField("base64 image", 500));
        Base64Image = base64Image;

        DomainValidationException.When(categoryId <= 0, ValidationMessageBuilder.GreaterThanZero("category id"));
        CategoryId = categoryId;

        DomainValidationException.When(locationId <= 0, ValidationMessageBuilder.GreaterThanZero("location id"));
        LocationId = locationId;
    }

}