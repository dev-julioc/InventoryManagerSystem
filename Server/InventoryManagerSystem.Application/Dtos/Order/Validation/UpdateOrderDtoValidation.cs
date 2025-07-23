using FluentValidation;
using InventoryManagerSystem.Domain.Validator;

namespace InventoryManagerSystem.Application.Dtos.Order.Validation;

public class UpdateOrderDtoValidation : AbstractValidator<UpdateOrderDto>
{
    public UpdateOrderDtoValidation()
    {
        RuleFor(x => x.State)
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("state"))
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("state"));
        
        RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage(ValidationMessageBuilder.GreaterThanZero("order id"));
    }
}