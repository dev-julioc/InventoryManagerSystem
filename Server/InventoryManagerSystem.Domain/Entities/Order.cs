using InventoryManagerSystem.Domain.Constants;
using InventoryManagerSystem.Domain.Validator;

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
        Validation(quantity, price, totalAmount, productId, clientId);
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

    private void Validation(int quantity, decimal price, decimal totalAmount, int productId, string clientId)
    {
        DomainValidationException.When(quantity <= 0, ValidationMessageBuilder.GreaterThanZero("quantity"));
        Quantity = quantity;
        
        DomainValidationException.When(price <= 0, ValidationMessageBuilder.GreaterThanZero("price"));
        Price = price;
        
        DomainValidationException.When(totalAmount <= 0, ValidationMessageBuilder.GreaterThanZero("total amount"));
        TotalAmount = totalAmount;
        
        DomainValidationException.When(productId <= 0, ValidationMessageBuilder.GreaterThanZero("product id"));
        ProductId = productId;
        
        DomainValidationException.When(string.IsNullOrEmpty(clientId), ValidationMessageBuilder.RequiredField("client id"));
        ClientId = clientId;
    }


}