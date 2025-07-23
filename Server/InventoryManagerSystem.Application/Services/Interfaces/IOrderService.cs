using InventoryManagerSystem.Application.Dtos.Order;
using InventoryManagerSystem.Application.Services.Result;

namespace InventoryManagerSystem.Application.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<ReadOrderDto>> GetAllOrdersAsync();
    Task<IEnumerable<ReadOrderProductsWithQuantityDto>> GetOrderProductsWithQuantityAsync(string userId);
    Task<IEnumerable<ReadOrderDto>> GetOrderByUserIdAsync(string userId);
    Task<IEnumerable<ReadProductOrderByMonths>> GetOrderByMonthsAsync(string userId);
    Task<ResultService<ReadOrdersCountDto>> GetOrdersCountAsync(string userId, bool isAdmin);
    Task<ResultService> CancelOrderAsync(int orderId);
    Task<ResultService<ReadOrderDto>> AddOrderAsync(AddOrderDto orderDto);
    Task<ResultService> UpdateOrderAsync(UpdateOrderDto orderDto);
}