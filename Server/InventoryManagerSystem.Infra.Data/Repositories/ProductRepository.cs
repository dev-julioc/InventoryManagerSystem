using InventoryManagerSystem.Domain.Entities;
using InventoryManagerSystem.Domain.Interfaces;
using InventoryManagerSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerSystem.Infra.Data.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
        => await context.Products.AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Location).ToListAsync();
    

    public async Task<Product?> GetProductByIdAsync(int id)
    => await context.Products.AsNoTracking()
        .Include(x => x.Category)
        .Include(x => x.Location).FirstOrDefaultAsync(x => x.Id == id);

    public async Task<bool> IsProductAlreadyExistsAsync(string name)
        => await context.Products.AsNoTracking().AnyAsync(x => x.Name!.ToLower().Equals(name.ToLower()));

    public async Task<Product?> AddProductAsync(Product product)
    {
        context.Products.Add(product);
        var result = await context.SaveChangesAsync();
        return result > 0 ? product : null;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        context.Products.Update(product);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteProductAsync(Product product)
    {
        context.Products.Remove(product);
        return await context.SaveChangesAsync() > 0;
    }
}