using InventoryManagerSystem.Application.Dtos.Order;
using InventoryManagerSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerSystem.API.Routes.Order;

public static class OrderHandler
{
    /// <summary>
    /// Retorna todos as lojas.
    /// </summary>
    /// <returns>Uma lista de lojas.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Sem acesso.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> GetAll([FromServices] IOrderService orderService)
    {
        var result = await orderService.GetAllOrdersAsync();
        
        return Results.Ok(result);
    }

    /// <summary>
    /// Retorna uma loja por id.
    /// </summary>
    /// <param name="id">Loja id.</param>
    /// <param name="lojaService"></param>
    /// <returns>Loja requisitado.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Sem acesso.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> GetOrderProductsWithQuantity([FromServices] IOrderService orderService, [FromRoute] string userId)
    {
        var result = await orderService.GetOrderProductsWithQuantityAsync(userId);
        
        return Results.Ok(result);
    }
    
    /// <summary>
    /// Retorna uma loja por id.
    /// </summary>
    /// <param name="id">Loja id.</param>
    /// <param name="lojaService"></param>
    /// <returns>Loja requisitado.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Sem acesso.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> GetByUserId([FromServices] IOrderService orderService, [FromRoute] string userId)
    {
        var result = await orderService.GetOrderByUserIdAsync(userId);
        
        return Results.Ok(result);
    }
    
    /// <summary>
    /// Retorna uma loja por id.
    /// </summary>
    /// <param name="id">Loja id.</param>
    /// <param name="lojaService"></param>
    /// <returns>Loja requisitado.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Sem acesso.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> GetOrderByMonthsAsync([FromServices] IOrderService orderService, [FromRoute] string userId)
    {
        var result = await orderService.GetOrderByMonthsAsync(userId);
        
        return Results.Ok(result);
    }
    
    /// <summary>
    /// Retorna uma loja por id.
    /// </summary>
    /// <param name="id">Loja id.</param>
    /// <param name="lojaService"></param>
    /// <returns>Loja requisitado.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Sem acesso.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> GetOrdersCountAsync([FromServices] IOrderService orderService, string userId, bool isAdmin)
    {
        var result = await orderService.GetOrdersCountAsync(userId, isAdmin);
        
        return Results.Ok(result);
    }
    
    /// <summary>
    /// Retorna uma loja por id.
    /// </summary>
    /// <param name="id">Loja id.</param>
    /// <param name="lojaService"></param>
    /// <returns>Loja requisitado.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Sem acesso.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> CancelOrderAsync([FromServices] IOrderService orderService, [FromRoute] int orderId)
    {
        var result = await orderService.CancelOrderAsync(orderId);
        
        return !result.IsFound ? Results.NotFound(result)
            : Results.Ok(result);
    }

    /// <summary>
    /// Adiciona uma nova loja.
    /// </summary>
    /// <param name="lojaDto">Modelo para adicionar uma nova loja.</param>
    /// <param name="lojaService"></param>
    /// <returns>Loja adicionada.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Sem acesso.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> Post([FromServices] IOrderService orderService, [FromBody] AddOrderDto orderDto)
    {
        var result = await orderService.AddOrderAsync(orderDto);
        
        return !result.IsValid ? Results.BadRequest(result) 
            : Results.Ok(result);
    }

    /// <summary>
    /// Atualiza uma loja existente.
    /// </summary>
    /// <param name="id">Loja id.</param>
    /// <param name="lojaDto">Modelo para atualizar uma nova loja.</param>
    /// <param name="lojaService"></param>
    /// <returns>Loja atualizado.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Sem acesso.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> Put([FromServices] IOrderService orderService, [FromBody] UpdateOrderDto orderDto, [FromRoute] int id)
    {
        var result = await orderService.UpdateOrderAsync(orderDto);
        
        if(!result.IsFound)
            return Results.NotFound(result);
        
        return !result.IsValid ? Results.BadRequest(result)
            : Results.Ok(result);
    }
}