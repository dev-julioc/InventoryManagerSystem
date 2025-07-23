using InventoryManagerSystem.Domain.Entities;
using InventoryManagerSystem.Domain.Interfaces;
using InventoryManagerSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerSystem.Infra.Data.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
        => await context.Categories.AsNoTracking().ToListAsync();

    public async Task<Category?> GetCategoryByIdAsync(int id)
        => await context.Categories.FindAsync(id);

    public async Task<bool> IsCategoryExistsAsync(string name)
        => await context.Categories.AsNoTracking().AnyAsync(x => x.Name!.ToLower().Equals(name.ToLower()));

    public async Task<Category?> AddCategoryAsync(Category category)
    {
        context.Categories.Add(category);
        var result = await context.SaveChangesAsync();
        return result > 0 ? category : null;
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        context.Categories.Update(category);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCategoryAsync(Category category)
    {
        context.Categories.Remove(category);
        return await context.SaveChangesAsync() > 0;
    }
}