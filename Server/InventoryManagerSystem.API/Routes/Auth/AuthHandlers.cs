using InventoryManagerSystem.Application.Services.Interfaces;
using InventoryManagerSystem.Shared.Auth;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerSystem.API.Routes.Auth;

public class AuthHandlers
{
    /// <summary>
    /// Registrar um novo usuário.
    /// </summary>
    /// <param name="authService"></param>
    /// <param name="registerDto">Modelo para registrar um novo usuário.</param>
    /// <returns></returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="400">Erro de validação.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> Register([FromServices] IAuthService authService, [FromBody] RegisterRequestDto registerDto)
    {
        var result = await authService.RegisterAsync(registerDto);
        
        return result.IsValid
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }

    /// <summary>
    /// Efetuar login e receber o token.
    /// </summary>
    /// <param name="authService"></param>
    /// <param name="loginDto">Modelo para efetuar o login.</param>
    /// <returns>Jwt token.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="400">Erro de validação.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> Login([FromServices] IAuthService authService, [FromBody] LoginRequestDto loginDto)
    {
        var result = await authService.LoginAsync(loginDto);
        
        return result.IsValid
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
    
    /// <summary>
    /// Efetuar login e receber o token.
    /// </summary>
    /// <param name="authService"></param>
    /// <param name="loginDto">Modelo para efetuar o login.</param>
    /// <returns>Jwt token.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="400">Erro de validação.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> Logout([FromServices] IAuthService authService)
    {
        await authService.LogoutAsync();

        return Results.Ok();
    }
    
    /// <summary>
    /// Efetuar login e receber o token.
    /// </summary>
    /// <param name="authService"></param>
    /// <param name="loginDto">Modelo para efetuar o login.</param>
    /// <returns>Jwt token.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="400">Erro de validação.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> GetAllUserClaims([FromServices] IAuthService authService)
    {
        var result = await authService.GetAllUsersWithClaimsAsync();

        return Results.Ok(result);
    }
    
    /// <summary>
    /// Efetuar login e receber o token.
    /// </summary>
    /// <param name="authService"></param>
    /// <param name="loginDto">Modelo para efetuar o login.</param>
    /// <returns>Jwt token.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="400">Erro de validação.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> SetUp([FromServices] IAuthService authService)
    {
        await authService.SetUpAsync();

        return Results.Ok();
    }
    
    /// <summary>
    /// Efetuar login e receber o token.
    /// </summary>
    /// <param name="authService"></param>
    /// <param name="loginDto">Modelo para efetuar o login.</param>
    /// <returns>Jwt token.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="400">Erro de validação.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> UpdateUser([FromServices] IAuthService authService, ChangeUserClaimRequestDto changeUserClaimRequestDto)
    {
        var result = await authService.UpdateUserAsync(changeUserClaimRequestDto);

        return result.IsValid
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}