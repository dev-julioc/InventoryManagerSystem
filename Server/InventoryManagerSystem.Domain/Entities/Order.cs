using InventoryManagerSystem.Domain.Constants;

namespace InventoryManagerSystem.Domain.Entities;

public sealed class Order
{
    public int Id { get; private set; }
    public DateTime DateOrdered { get; private set; }
    public DateTime? DeliveringDate { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string? OrderState { get; private set; }
    
    public int ProductId { get; private set; }
    public string? ClientId { get; private set; }

    public Product? Product { get; set; }

    public Order(int quantity, decimal price, decimal totalAmount, int productId, string clientId)
    {
        DateOrdered = DateTime.Now;
        DeliveringDate = null;
        OrderState = OrderStates.Processing;
    }
    
    public void CancelOrder() 
        => OrderState = OrderStates.Cancelled;
    
    public void Update(string orderState, DateTime deliveringDate)
    {
        OrderState = orderState;
        DeliveringDate = deliveringDate;
    }
}