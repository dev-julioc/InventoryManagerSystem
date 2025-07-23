using InventoryManagerSystem.Application.Dtos.Category;
using InventoryManagerSystem.Application.Services.Result;

namespace InventoryManagerSystem.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<ReadCategoryDto>> GetAllCategoriesAsync();
    Task<ResultService<ReadCategoryDto>> GetCategoryByIdAsync(int id);
    Task<ResultService<ReadCategoryDto>> AddCategoryAsync(AddCategoryDto categoryDto);
    Task<ResultService> UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    Task<ResultService> DeleteCategoryAsync(int id);
}