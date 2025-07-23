using InventoryManagerSystem.Domain.Entities;
using InventoryManagerSystem.Domain.Interfaces;
using InventoryManagerSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerSystem.Infra.Data.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        => await context.Orders.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        => await context.Orders.AsNoTracking().Where(x => x.ClientId!.Equals(userId)).ToListAsync();

    public async Task<Order?> GetOrderByIdAsync(int id)
        => await context.Orders.FindAsync(id);

    public async Task<Order?> AddOrderAsync(Order order)
    {
        context.Orders.Add(order);
        var result = await context.SaveChangesAsync();
        return result > 0 ? order : null;
    }

    public async Task<bool> UpdateOrderAsync(Order order)
    {
        context.Orders.Update(order);
        return await context.SaveChangesAsync() > 0;
    }
}