using System.Globalization;
using InventoryManagerSystem.Application.Dtos.Order;
using InventoryManagerSystem.Application.Services.Interfaces;
using InventoryManagerSystem.Application.Services.Result;
using InventoryManagerSystem.Domain.Constants;
using InventoryManagerSystem.Domain.Entities;
using InventoryManagerSystem.Domain.Interfaces;
using InventoryManagerSystem.Shared.Auth;

namespace InventoryManagerSystem.Application.Services;

public class OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IAuthManagement authManagement) : IOrderService
{
    public async Task<IEnumerable<ReadOrderDto>> GetAllOrdersAsync()
    {
        var orders = await orderRepository.GetAllOrdersAsync();
        
        var products = await productRepository.GetAllProductsAsync();
        
        var users = await authManagement.GetAllUsersAsync();
        
        return orders.Select(x => new ReadOrderDto
        (
            x.Id,
            x.OrderState!,
            products.FirstOrDefault(p => p.Id == x.ProductId)?.Name ?? "Unknown Product",
            products.FirstOrDefault(p => p.Id == x.ProductId)?.SerialNumber ?? "Unknown Serial",
            x.DateOrdered,
            x.DeliveringDate ?? DateTime.MinValue,
            x.Price,
            products.FirstOrDefault(p => p.Id == x.ProductId)?.Base64Image ?? string.Empty,
            x.ClientId!,
            users.FirstOrDefault(u => u.Id.Equals(x.ClientId))?.Name ?? "Unknown User",
            x.ProductId,
            x.Quantity
        ));
    }

    public async Task<IEnumerable<ReadOrderProductsWithQuantityDto>> GetOrderProductsWithQuantityAsync(string userId)
    {
        List<Order> orders;
        var data = new List<ReadOrderProductsWithQuantityDto>();
        
        orders = !string.IsNullOrEmpty(userId) 
            ? (await orderRepository.GetOrdersByUserIdAsync(userId)).ToList() 
            : (await orderRepository.GetAllOrdersAsync()).ToList();
        
        var selectOrdersWithin12Months = orders
                                                        .Where(order => order.DateOrdered.Date >= DateTime.Now.AddMonths(-12))
                                                        .GroupBy(order => new {Name = order.ProductId})
                                                        .ToList();

        foreach (var order in selectOrdersWithin12Months)
        {
            data.Add(
                new ReadOrderProductsWithQuantityDto
                (
                    (await productRepository.GetProductByIdAsync(order.Key.Name))!.Name!,
                    order.Sum(x => x.Quantity)
                ));
        }

        return data;
    }

    public async Task<IEnumerable<ReadOrderDto>> GetOrderByUserIdAsync(string userId)
    {
        var orders = await orderRepository.GetOrdersByUserIdAsync(userId);
        
        var products = await productRepository.GetAllProductsAsync();
        
        return orders.Select(x => new ReadOrderDto
        (
            x.Id,
            x.OrderState!,
            products.FirstOrDefault(p => p.Id == x.ProductId)?.Name ?? "Unknown Product",
            products.FirstOrDefault(p => p.Id == x.ProductId)?.SerialNumber ?? "Unknown Serial",
            x.DateOrdered,
            x.DeliveringDate ?? DateTime.MinValue,
            x.Price,
            products.FirstOrDefault(p => p.Id == x.ProductId)?.Base64Image ?? string.Empty,
            x.ClientId!,
            null,
            x.ProductId,
            x.Quantity
        ));
    }

    public async Task<IEnumerable<ReadProductOrderByMonths>> GetOrderByMonthsAsync(string userId)
    {
        List<Order> orders;
        var data = new List<ReadProductOrderByMonths>();
        
        orders = !string.IsNullOrEmpty(userId) 
            ? (await orderRepository.GetOrdersByUserIdAsync(userId)).ToList() 
            : (await orderRepository.GetAllOrdersAsync()).ToList();
        
        var selectOrdersWithin12Months = orders
                                                        .Where(order => order.DateOrdered.Date >= DateTime.Now.AddMonths(-12))
                                                        .GroupBy(order => new {order.DateOrdered.Month})
                                                        .ToList();

        foreach (var order in selectOrdersWithin12Months)
        {
            data.Add(new ReadProductOrderByMonths
                (
                    CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(order.Key.Month),
                    order.Sum(x => x.TotalAmount)
                ));
        }
        
        return data;
    }

    public async Task<ResultService<ReadOrdersCountDto>> GetOrdersCountAsync(string userId, bool isAdmin)
    {
        List<Order> orders;

        orders = !isAdmin 
            ? (await orderRepository.GetOrdersByUserIdAsync(userId)).ToList() 
            : (await orderRepository.GetAllOrdersAsync()).ToList();
        
        int processingCount = orders.Count(x => x.OrderState == OrderStates.Processing);
        int deliveringCount = orders.Count(x => x.OrderState == OrderStates.Delivering);
        int deliveredCount = orders.Count(x => x.OrderState == OrderStates.Delivered);
        int canceledCount = orders.Count(x => x.OrderState == OrderStates.Cancelled);
        
        return ResultService.Ok(new ReadOrdersCountDto(processingCount, deliveringCount, deliveredCount, canceledCount)); 
    }

    public async Task<ResultService> CancelOrderAsync(int orderId)
    {
        var order = await orderRepository.GetOrderByIdAsync(orderId);
        
        if(order is null)
            return ResultService.Fail("Order not found.");
        
        order.CancelOrder();

        var result = await orderRepository.UpdateOrderAsync(order);
        
        return !result
            ? ResultService.Fail<ReadOrderDto>("Failed to update order.")
            : ResultService.Ok("Order cancelled successfully.");
    }

    public async Task<ResultService<ReadOrderDto>> AddOrderAsync(AddOrderDto orderDto)
    {
        var product = await productRepository.GetProductByIdAsync(orderDto.ProductId);
        
        if(product is null)
            return ResultService.Fail<ReadOrderDto>("Product not found.");

        var order = new Order(orderDto.Quantity, product.Price, product.Price * orderDto.Quantity, orderDto.ProductId, orderDto.ClientId);
        
        var result = await orderRepository.AddOrderAsync(order);
        
        if(result is null)
            return ResultService.Fail<ReadOrderDto>("Failed to add order.");
        
        var readOrderDto = new ReadOrderDto(
            result.Id,
            order.OrderState!,
            product.Name!,
            product.SerialNumber!,
            order.DateOrdered,
            order.DeliveringDate ?? DateTime.MinValue,
            product.Price,
            product.Base64Image!,
            order.ClientId!,
            order.ClientId!,
            order.ProductId,
            order.Quantity
        );

        return ResultService.Ok(readOrderDto);
    }

    public async Task<ResultService> UpdateOrderAsync(UpdateOrderDto orderDto)
    {
        var order = await orderRepository.GetOrderByIdAsync(orderDto.OrderId);
        
        if(order is null)
            return ResultService.NotFound("Order not found.");
        
        order.Update(orderDto.State, orderDto.DeliveringDate);
        
        var result = await orderRepository.UpdateOrderAsync(order);
        
        return !result
            ? ResultService.Fail("Failed to update order.")
            : ResultService.Ok("Order updated successfully.");
    }
}