using InventoryManagerSystem.Application.Dtos.Category;
using InventoryManagerSystem.Application.Dtos.Location;
using InventoryManagerSystem.Application.Dtos.Product;
using InventoryManagerSystem.Domain.Entities;

namespace InventoryManagerSystem.Application.Map;

public static class ProductMapping
{
    public static Product ConvertToEntity(this AddProductDto productDto)
        => new(productDto.Name, productDto.SerialNumber, productDto.Price, productDto.Quantity,
            productDto.Description, productDto.Base64Image, productDto.CategoryId, productDto.LocationId);
    
    public static ReadProductDto ConvertToDto(this Product product)
    => new ReadProductDto(product.Id, product.Name!, product.SerialNumber!, product.Price, product.Quantity, product.Description!, product.Base64Image!, 
        product.DateAdded, product.CategoryId, product.LocationId, 
        new ReadCategoryDto(product.Category!.Id, product.Category.Name!), 
        new ReadLocationDto(product.Location!.Id, product.Location.Name!));
    
    public static IEnumerable<ReadProductDto> ConvertToDtoList(this IEnumerable<Product> products)
        => products.Select(x => x.ConvertToDto());
}