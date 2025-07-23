using InventoryManagerSystem.Application.Dtos.Location;
using InventoryManagerSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerSystem.API.Routes.Location;

public static class LocationHandlers
{
     /// <summary>
    /// Retorna todos as lojas.
    /// </summary>
    /// <returns>Uma lista de lojas.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Sem acesso.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> GetAll([FromServices] ILocationService locationService)
    {
        var result = await locationService.GetAllLocationAsync();
        
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
    public static async Task<IResult> GetById([FromServices] ILocationService locationService, [FromRoute] int id)
    {
        var result = await locationService.GetLocationByIdAsync(id);
        
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
    public static async Task<IResult> Post([FromServices] ILocationService locationService, [FromBody] AddLocationDto locationDto)
    {
        var result = await locationService.AddLocationAsync(locationDto);
        
        return !result.IsValid ? Results.BadRequest(result) 
            : Results.CreatedAtRoute("GetLocationById", new { id = result.Data.Id }, result);
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
    public static async Task<IResult> Put([FromServices] ILocationService locationService, [FromBody] UpdateLocationDto locationDto, [FromRoute] int id)
    {
        var result = await locationService.UpdateLocationAsync(locationDto);
        
        if(!result.IsFound)
            return Results.NotFound(result);
        
        return !result.IsValid ? Results.BadRequest(result)
            : Results.Ok(result);
    }

    /// <summary>
    /// Deleta uma loja por id.
    /// </summary>
    /// <param name="id">Loja id.</param>
    /// <param name="lojaService"></param>
    /// <returns>Loja deletado.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Sem acesso.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> Delete([FromServices] ILocationService locationService, [FromRoute] int id)
    {
        var result = await locationService.DeleteLocationAsync(id);
        
        if(!result.IsFound)
            return Results.NotFound(result);
        
        return !result.IsValid ? Results.BadRequest(result)
            : Results.Ok(result);
    }
}