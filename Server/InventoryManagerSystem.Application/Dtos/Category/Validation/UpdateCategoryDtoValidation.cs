using FluentValidation;
using InventoryManagerSystem.Domain.Validator;

namespace InventoryManagerSystem.Application.Dtos.Category.Validation;

public class UpdateCategoryDtoValidation : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage(ValidationMessageBuilder.GreaterThanZero("id"));
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("name"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("name"))
            .Length(3, 150).WithMessage(ValidationMessageBuilder.RangeField("name", 3, 150));
    }
}