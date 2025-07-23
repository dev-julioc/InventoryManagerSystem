using Carter;
using InventoryManagerSystem.API.Filters;
using InventoryManagerSystem.Application.Dtos.Location;
using InventoryManagerSystem.Application.Services.Result;

namespace InventoryManagerSystem.API.Routes.Location;

public class LocationRoutes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/locations")
            .WithTags("Locations");
        
        endpoints.MapGet("/", LocationHandlers.GetAll)
            .Produces<IEnumerable<ReadLocationDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapGet("/{id}", LocationHandlers.GetById)
            .Produces<ResultService<ReadLocationDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetLocationById");
        
        endpoints.MapPost("/", LocationHandlers.Post)
            .Produces<ResultService<ReadLocationDto>>(StatusCodes.Status201Created)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter<AddLocationDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapPut("/{id}", LocationHandlers.Put)
            .Produces<ResultService>()
            .AddEndpointFilter<ValidationFilter<UpdateLocationDto>>()
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<ResultService>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapDelete("/{id}", LocationHandlers.Delete)
            .Produces<ResultService>()
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<ResultService>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}