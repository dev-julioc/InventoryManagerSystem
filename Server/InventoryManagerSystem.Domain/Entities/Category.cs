using InventoryManagerSystem.Domain.Validator;

namespace InventoryManagerSystem.Domain.Entities;

public sealed class Category
{
    public int Id { get; private set; }
    public string? Name { get; private set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();

    private Category() {}

    public Category(string name)
    {
        Validation(name);
    }
    
    public void Update(string name) 
        => Name = name;

    private void Validation(string name)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), ValidationMessageBuilder.RequiredField("name"));
        DomainValidationException.When(name.Length is < 3 or > 150, ValidationMessageBuilder.RangeField("name", 3, 150));
        Name = name;
    }
}