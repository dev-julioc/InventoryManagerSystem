using FluentValidation;
using InventoryManagerSystem.Domain.Validator;
using InventoryManagerSystem.Shared.Auth;

namespace InventoryManagerSystem.Application.Dtos.Auth.Validation;

public class RegisterDtoValidation : AbstractValidator<RegisterRequestDto>
{
    public RegisterDtoValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("nome"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("nome"))
            .Length(3, 100).WithMessage(ValidationMessageBuilder.RangeField("nome", 3, 100));

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("email"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("email"));
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("senha"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("senha"))
            .MinimumLength(6).WithMessage(ValidationMessageBuilder.MinLengthField("senha", 6))
            .Matches("[A-Z]").WithMessage("Senha deve ter pelo menos uma letra maiúscula.")
            .Matches("[a-z]").WithMessage("Senha deve ter pelo menos uma letra minúscula.")
            .Matches("[0-9]").WithMessage("Senha deve ter pelo menos um número.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Senha deve ter pelo menos um caractere especial.");
        
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("As senhas não coincidem.");
        
        RuleFor(x => x.Policy)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("policy"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("policy"));
    }
}