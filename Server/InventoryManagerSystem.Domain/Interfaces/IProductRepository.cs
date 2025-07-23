using InventoryManagerSystem.Domain.Entities;

namespace InventoryManagerSystem.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<bool> IsProductAlreadyExistsAsync(string name);
    Task<Product?> AddProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Product product);
}