using Carter;
using InventoryManagerSystem.API.Filters;
using InventoryManagerSystem.Application.Services.Result;
using InventoryManagerSystem.Shared.Auth;

namespace InventoryManagerSystem.API.Routes.Auth;

public class AuthRoutes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/auth");
        
        endpoints.MapPost("/register", AuthHandlers.Register)
            .AddEndpointFilter<ValidationFilter<RegisterRequestDto>>()
            .Produces(StatusCodes.Status200OK)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapPost("/login", AuthHandlers.Login)
            .AddEndpointFilter<ValidationFilter<LoginRequestDto>>()
            .Produces(StatusCodes.Status200OK)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapPost("/logout", AuthHandlers.Logout)
            .Produces(StatusCodes.Status200OK)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapGet("/users-claims", AuthHandlers.GetAllUserClaims)
            .Produces(StatusCodes.Status200OK)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapPatch("/set-up", AuthHandlers.SetUp)
            .Produces(StatusCodes.Status200OK)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapPut("/user", AuthHandlers.UpdateUser)
            .AddEndpointFilter<ValidationFilter<ChangeUserClaimRequestDto>>()
            .Produces(StatusCodes.Status200OK)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapPost("/activity", AuthHandlers.SaveActivity)
            .Produces(StatusCodes.Status200OK)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        endpoints.MapGet("/activity", AuthHandlers.GetGroupActivities)
            .Produces(StatusCodes.Status200OK)
            .Produces<ResultService>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}