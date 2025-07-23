using FluentValidation;
using InventoryManagerSystem.Domain.Validator;

namespace InventoryManagerSystem.Application.Dtos.Order.Validation;

public class AddOrderDtoValidation : AbstractValidator<AddOrderDto>
{
    public AddOrderDtoValidation()
    {
        RuleFor(x => x.ClientId)
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("client id"))
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("client id"));
        
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage(ValidationMessageBuilder.GreaterThanZero("product id"));
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage(ValidationMessageBuilder.GreaterThanZero("quantity"));
    }
}