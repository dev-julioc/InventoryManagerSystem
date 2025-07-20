using FluentValidation;
using InventoryManagerSystem.Application.Services.Result;

namespace InventoryManagerSystem.API.Filters;

public class ValidationFilter<TRequest>(IValidator<TRequest> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<TRequest>().First();
        
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var erroResult =  ResultService.RequestError("Erro de validação", result);
            return Results.BadRequest(erroResult);
        }
        
        return await next(context);
    }
}