using FluentValidation;
using InventoryManagerSystem.Domain.Validator;

namespace InventoryManagerSystem.Application.Dtos.Product.Validation;

public class UpdateProductDtoValidation : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage(ValidationMessageBuilder.GreaterThanZero("id"));
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("name"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("name"))
            .Length(3, 150).WithMessage(ValidationMessageBuilder.RangeField("name", 3, 150));
        
        RuleFor(x => x.SerialNumber)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("serial number"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("serial number"))
            .Length(3, 250).WithMessage(ValidationMessageBuilder.RangeField("serial number", 3, 250));

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage(ValidationMessageBuilder.GreaterThanZero("price"));
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage(ValidationMessageBuilder.GreaterThanZero("quantity"));
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("description"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("description"))
            .Length(3, 250).WithMessage(ValidationMessageBuilder.RangeField("description", 3, 250));
        
        RuleFor(x => x.Base64Image)
            .NotEmpty().WithMessage(ValidationMessageBuilder.RequiredField("base64 image"))
            .NotNull().WithMessage(ValidationMessageBuilder.RequiredField("base64 image"))
            .Length(3, 500).WithMessage(ValidationMessageBuilder.RangeField("base64 image", 3, 500));
        
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage(ValidationMessageBuilder.GreaterThanZero("category id"));
        
        RuleFor(x => x.LocationId)
            .GreaterThan(0).WithMessage(ValidationMessageBuilder.GreaterThanZero("location id"));
    }
}