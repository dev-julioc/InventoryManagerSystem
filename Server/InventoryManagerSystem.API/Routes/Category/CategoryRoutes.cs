using Carter;
using InventoryManagerSystem.API.Filters;
using InventoryManagerSystem.Application.Dtos.Category;
using InventoryManagerSystem.Application.Dtos.Location;
using InventoryManagerSystem.Application.Services.Result;

namespace InventoryManagerSystem.API.Routes.Category;

public class CategoryRoutes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/categories")
            .WithTags("Categories");
        
        endpoints.MapGet("/", CategoryHandler.GetAll)
            .Produces<IEnumerable<ReadCategoryDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapGet("/{id}", CategoryHandler.GetById)
            .Produces<ResultService<ReadCategoryDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetCategoryById");
        
        endpoints.MapPost("/", CategoryHandler.Post)
            .Produces<ResultService<ReadCategoryDto>>(StatusCodes.Status201Created)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter<AddCategoryDto>>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapPut("/{id}", CategoryHandler.Put)
            .Produces<ResultService>()
            .AddEndpointFilter<ValidationFilter<UpdateCategoryDto>>()
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<ResultService>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapDelete("/{id}", CategoryHandler.Delete)
            .Produces<ResultService>()
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<ResultService>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}