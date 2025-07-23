using Carter;
using InventoryManagerSystem.API.Filters;
using InventoryManagerSystem.Application.Dtos.Order;
using InventoryManagerSystem.Application.Services.Result;

namespace InventoryManagerSystem.API.Routes.Order;

public class OrderRoutes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
         var endpoints = app.MapGroup("api/orders")
            .WithTags("Orders");
        
        endpoints.MapGet("/", OrderHandler.GetAll)
            .Produces<IEnumerable<ReadOrderDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapGet("/product-quantity/user/{userId}", OrderHandler.GetOrderProductsWithQuantity)
            .Produces<IEnumerable<ReadOrderProductsWithQuantityDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapGet("/user/{userId}", OrderHandler.GetByUserId)
            .Produces<IEnumerable<ReadOrderDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapGet("/month/user/{userId}", OrderHandler.GetOrderByMonthsAsync)
            .Produces<IEnumerable<ReadProductOrderByMonths>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapGet("/count", OrderHandler.GetOrdersCountAsync)
            .Produces<ResultService<ReadOrdersCountDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapPatch("/{orderId}/cancel", OrderHandler.CancelOrderAsync)
            .Produces<ResultService>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapPost("/", OrderHandler.Post)
            .AddEndpointFilter<ValidationFilter<AddOrderDto>>()
            .Produces<ResultService<ReadOrderDto>>(StatusCodes.Status201Created)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapPut("/{id}", OrderHandler.Put)
            .Produces<ResultService>()
            .AddEndpointFilter<ValidationFilter<UpdateOrderDto>>()
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<ResultService>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}