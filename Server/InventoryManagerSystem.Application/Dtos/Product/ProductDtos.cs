using InventoryManagerSystem.Application.Dtos.Category;
using InventoryManagerSystem.Application.Dtos.Location;

namespace InventoryManagerSystem.Application.Dtos.Product;

public record AddProductDto(string Name, string SerialNumber, decimal Price, int Quantity,
    string Description, string Base64Image, int CategoryId, int LocationId);

public record UpdateProductDto(int Id, string Name, string SerialNumber, decimal Price, int Quantity,
    string Description, string Base64Image, DateTime DateAdded, int CategoryId, int LocationId);

public record ReadProductDto(int Id, string Name, string SerialNumber, decimal Price, int Quantity,
    string Description, string Base64Image, DateTime DateAdded, int CategoryId, int LocationId, ReadCategoryDto Category, ReadLocationDto Location);
    