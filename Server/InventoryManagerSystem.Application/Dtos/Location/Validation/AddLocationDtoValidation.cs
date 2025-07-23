using FluentValidation;
using InventoryManagerSystem.Domain.Validator;

namespace InventoryManagerSystem.Application.Dtos.Location.Validation;

public class AddLocationDtoValidation : AbstractValidator<AddLocationDto>
{
    public AddLocationDtoValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("name"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("name"))
            .Length(3, 150).WithMessage(ValidationMessageBuilder.RangeField("name", 3, 150));
    }
}