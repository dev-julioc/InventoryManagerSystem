using InventoryManagerSystem.Application.Dtos.Category;
using InventoryManagerSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerSystem.API.Routes.Category;

public static class CategoryHandler
{
      /// <summary>
    /// Retorna todos as lojas.
    /// </summary>
    /// <returns>Uma lista de lojas.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Sem acesso.</response>
    /// <response code="500">Erro interno.</response>
    public static async Task<IResult> GetAll([FromServices] ICategoryService categoryService)
    {
        var result = await categoryService.GetAllCategoriesAsync();
        
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
    public static async Task<IResult> GetById([FromServices] ICategoryService categoryService, [FromRoute] int id)
    {
        var result = await categoryService.GetCategoryByIdAsync(id);
        
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
    public static async Task<IResult> Post([FromServices] ICategoryService categoryService, [FromBody] AddCategoryDto categoryDto)
    {
        var result = await categoryService.AddCategoryAsync(categoryDto);
        
        return !result.IsValid ? Results.BadRequest(result) 
            : Results.CreatedAtRoute("GetCategoryById", new { id = result.Data.Id }, result);
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
    public static async Task<IResult> Put([FromServices] ICategoryService categoryService, [FromBody] UpdateCategoryDto categoryDto, [FromRoute] int id)
    {
        var result = await categoryService.UpdateCategoryAsync(categoryDto);
        
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
    public static async Task<IResult> Delete([FromServices] ICategoryService categoryService, [FromRoute] int id)
    {
        var result = await categoryService.DeleteCategoryAsync(id);
        
        if(!result.IsFound)
            return Results.NotFound(result);
        
        return !result.IsValid ? Results.BadRequest(result)
            : Results.Ok(result);
    }
}