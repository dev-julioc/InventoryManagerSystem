using Carter;
using InventoryManagerSystem.API.Filters;
using InventoryManagerSystem.Application.Dtos.Product;
using InventoryManagerSystem.Application.Services.Result;

namespace InventoryManagerSystem.API.Routes.Product;

public class ProductRoutes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
         var endpoints = app.MapGroup("api/products")
            .WithTags("Products");
        
        endpoints.MapGet("/", ProductHandler.GetAll)
            .Produces<IEnumerable<ReadProductDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapGet("/{id}", ProductHandler.GetById)
            .Produces<ResultService<ReadProductDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetProductById");
        
        endpoints.MapPost("/", ProductHandler.Post)
            .Produces<ResultService<ReadProductDto>>(StatusCodes.Status201Created)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter<AddProductDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapPut("/{id}", ProductHandler.Put)
            .Produces<ResultService>()
            .AddEndpointFilter<ValidationFilter<UpdateProductDto>>()
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<ResultService>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapDelete("/{id}", ProductHandler.Delete)
            .Produces<ResultService>()
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<ResultService>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}