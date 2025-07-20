using FluentValidation;
using InventoryManagerSystem.Domain.Validator;
using InventoryManagerSystem.Shared.Auth;

namespace InventoryManagerSystem.Application.Dtos.Auth.Validation;

public class UpdateUserDtoValidation : AbstractValidator<ChangeUserClaimRequestDto>
{
    public UpdateUserDtoValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("user id"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("user id"));
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("name"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("name"))
            .Length(3, 100).WithMessage(ValidationMessageBuilder.RangeField("name", 3, 100));
        
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("role"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("role"));
    }
}