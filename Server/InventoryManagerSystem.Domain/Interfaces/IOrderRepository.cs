using InventoryManagerSystem.Domain.Entities;

namespace InventoryManagerSystem.Domain.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
    Task<Order?> GetOrderByIdAsync(int id);
    Task<Order?> AddOrderAsync(Order order);
    Task<bool> UpdateOrderAsync(Order order);
}