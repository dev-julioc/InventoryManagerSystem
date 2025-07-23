using InventoryManagerSystem.Application.Dtos.Product;
using InventoryManagerSystem.Application.Services.Result;

namespace InventoryManagerSystem.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ReadProductDto>> GetAllProductsAsync();
    Task<ResultService<ReadProductDto>> GetProductByIdAsync(int id);
    Task<ResultService<ReadProductDto>> AddProductAsync(AddProductDto productDto);
    Task<ResultService> UpdateProductAsync(UpdateProductDto productDto);
    Task<ResultService> DeleteProductAsync(int id);
}