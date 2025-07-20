using FluentValidation;
using InventoryManagerSystem.Domain.Validator;
using InventoryManagerSystem.Shared.Auth;

namespace InventoryManagerSystem.Application.Dtos.Auth.Validation;

public class LoginDtoValidation : AbstractValidator<LoginRequestDto>
{
    public LoginDtoValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("email"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("email"));
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("senha"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("senha"));
    }
}