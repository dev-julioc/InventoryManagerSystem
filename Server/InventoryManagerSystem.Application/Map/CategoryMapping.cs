using InventoryManagerSystem.Application.Dtos.Category;
using InventoryManagerSystem.Domain.Entities;

namespace InventoryManagerSystem.Application.Map;

public static class CategoryMapping
{
    public static Category ConvertToEntity(this AddCategoryDto categoryDto)
        => new (categoryDto.Name);

    public static ReadCategoryDto ConvertToDto(this Category category)
        => new(category.Id, category.Name!);

    public static IEnumerable<ReadCategoryDto> ConvertToDtoList(this IEnumerable<Category> categories)
        => categories.Select(x => x.ConvertToDto());
}