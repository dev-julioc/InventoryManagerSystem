using InventoryManagerSystem.Domain.Entities;

namespace InventoryManagerSystem.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<bool> IsCategoryExistsAsync(string name);
    Task<Category?> AddCategoryAsync(Category category);
    Task<bool> UpdateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(Category category);
}