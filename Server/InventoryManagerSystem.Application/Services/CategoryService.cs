using InventoryManagerSystem.Application.Dtos.Category;
using InventoryManagerSystem.Application.Map;
using InventoryManagerSystem.Application.Services.Interfaces;
using InventoryManagerSystem.Application.Services.Result;
using InventoryManagerSystem.Domain.Interfaces;

namespace InventoryManagerSystem.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<IEnumerable<ReadCategoryDto>> GetAllCategoriesAsync()
    {
        var result = await categoryRepository.GetCategoriesAsync();
        return result.ConvertToDtoList();
    }

    public async Task<ResultService<ReadCategoryDto>> GetCategoryByIdAsync(int id)
    {
        var result = await categoryRepository.GetCategoryByIdAsync(id);
        
        return result is null ? ResultService.NotFound<ReadCategoryDto>("Category not found.")
                              : ResultService.Ok(result.ConvertToDto());
    }

    public async Task<ResultService<ReadCategoryDto>> AddCategoryAsync(AddCategoryDto categoryDto)
    {
        if (await categoryRepository.IsCategoryExistsAsync(categoryDto.Name))
            return ResultService.Fail<ReadCategoryDto>("Category already exists.");

        var category = categoryDto.ConvertToEntity();
        
        var result = await categoryRepository.AddCategoryAsync(category);

        return result is null ? ResultService.Fail<ReadCategoryDto>("Failed to add category.")
                              : ResultService.Ok(result.ConvertToDto());
    }

    public async Task<ResultService> UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        var category = await categoryRepository.GetCategoryByIdAsync(categoryDto.Id);
        
        if(category is null)
            return ResultService.NotFound("Category not found.");

        if (!category.Name.Equals(categoryDto.Name) &&
            await categoryRepository.IsCategoryExistsAsync(categoryDto.Name))
            return ResultService.Fail("Category already exists.");
        
        category.Update(categoryDto.Name);
        
        var result = await categoryRepository.UpdateCategoryAsync(category);

        return !result
            ? ResultService.Fail("Failed to update category.")
            : ResultService.Ok("Category updated successfully.");

    }

    public async Task<ResultService> DeleteCategoryAsync(int id)
    {
        var category = await categoryRepository.GetCategoryByIdAsync(id);
        
        if(category is null)
            return ResultService.NotFound("Category not found.");
        
        var result = await categoryRepository.DeleteCategoryAsync(category);
        
        return !result
            ? ResultService.Fail("Failed to delete category.")
            : ResultService.Ok("Category deleted successfully.");
    }
}