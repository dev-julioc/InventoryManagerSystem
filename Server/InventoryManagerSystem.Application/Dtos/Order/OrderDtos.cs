namespace InventoryManagerSystem.Application.Dtos.Order;

public record AddOrderDto(string ClientId, int ProductId, int Quantity);

public record UpdateOrderDto(int OrderId, string State, DateTime DeliveringDate);

public record ReadOrderProductsWithQuantityDto(string ProductName, int QuantityOrdered);

public record GenericOrdersCountDto(string UserId, bool IsAdmin);

public record ReadOrderDto(
    int Id,
    string State,
    string ProductName,
    string SerialNumber,
    DateTime OrderDate,
    DateTime DeliveringDate,
    decimal Price,
    string ProductImage,
    string ClientId,
    string? ClientName,
    int ProductId,
    int Quantity)
{
    public decimal TotalAmount => Price * Quantity;
};


public record ReadOrdersCountDto(int Processing, int Delivering, int Delivered, int Cancelled);

public record ReadProductOrderByMonths(string MonthName, decimal TotalAmount)
{
    public string FormattedTotalAmount => TotalAmount.ToString("#, ##0.00");
};