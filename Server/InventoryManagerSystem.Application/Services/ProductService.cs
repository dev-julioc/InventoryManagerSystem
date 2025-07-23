using InventoryManagerSystem.Application.Dtos.Product;
using InventoryManagerSystem.Application.Map;
using InventoryManagerSystem.Application.Services.Interfaces;
using InventoryManagerSystem.Application.Services.Result;
using InventoryManagerSystem.Domain.Interfaces;

namespace InventoryManagerSystem.Application.Services;

public class ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, ILocationRepository locationRepository) : IProductService
{
    public async Task<IEnumerable<ReadProductDto>> GetAllProductsAsync()
    {
        var result = await productRepository.GetAllProductsAsync();

        return result.ConvertToDtoList();
    }

    public async Task<ResultService<ReadProductDto>> GetProductByIdAsync(int id)
    {
        var result = await productRepository.GetProductByIdAsync(id);
        
        return result is null
            ? ResultService.NotFound<ReadProductDto>("Product not found.") 
            : ResultService.Ok(result.ConvertToDto());
    }

    public async Task<ResultService<ReadProductDto>> AddProductAsync(AddProductDto productDto)
    {
        if(await productRepository.IsProductAlreadyExistsAsync(productDto.Name))
            return ResultService.Fail<ReadProductDto>("Product already exists.");
        
        if(await categoryRepository.GetCategoryByIdAsync(productDto.CategoryId) is null)
            return ResultService.Fail<ReadProductDto>("Category not found.");
        
        if(await locationRepository.GetLocationByIdAsync(productDto.LocationId) is null)
            return ResultService.Fail<ReadProductDto>("Location not found.");
        
        var product = productDto.ConvertToEntity();
        
        var result = await productRepository.AddProductAsync(product);
        
        return result is null
            ? ResultService.Fail<ReadProductDto>("Failed to add product.")
            : ResultService.Ok(result.ConvertToDto());
    }

    public async Task<ResultService> UpdateProductAsync(UpdateProductDto productDto)
    {
        var product = await productRepository.GetProductByIdAsync(productDto.Id);
        
        if (product is null)
            return ResultService.NotFound("Product not found.");
        
        if(!product.Name.Equals(productDto.Name) && 
           await productRepository.IsProductAlreadyExistsAsync(productDto.Name))
            return ResultService.Fail<ReadProductDto>("Product already exists.");
        
        if(!product.CategoryId.Equals(productDto.CategoryId) && 
           await categoryRepository.GetCategoryByIdAsync(productDto.CategoryId) is null)
            return ResultService.Fail<ReadProductDto>("Category not found.");
        
        if(!product.LocationId.Equals(productDto.LocationId) && 
           await locationRepository.GetLocationByIdAsync(productDto.LocationId) is null)
            return ResultService.Fail<ReadProductDto>("Location not found.");
        
        product.Update(productDto.Name, productDto.SerialNumber, productDto.Price, productDto.Quantity, productDto.Description, productDto.Base64Image, 
            productDto.DateAdded, productDto.CategoryId, productDto.LocationId);
        
        var result = await productRepository.UpdateProductAsync(product);
        
        return !result 
            ? ResultService.Fail("Failed to update product.") 
            : ResultService.Ok("Product updated successfully.");
    }

    public async Task<ResultService> DeleteProductAsync(int id)
    {
        var product = await productRepository.GetProductByIdAsync(id);
        
        if (product is null)
            return ResultService.NotFound("Product not found.");
        
        var result = await productRepository.DeleteProductAsync(product);
        
        return !result 
            ? ResultService.Fail("Failed to delete product.") 
            : ResultService.Ok("Product deleted successfully.");
    }
}