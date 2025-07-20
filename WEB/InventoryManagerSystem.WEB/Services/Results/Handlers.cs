using System.Net;
using Newtonsoft.Json;

namespace InventoryManagerSystem.WEB.Services.Results;

public class Handlers
{
    public static ResultService ErrorResponse(HttpStatusCode statusCode, string jsonResponse)
    {
        var apiResponse = JsonConvert.DeserializeObject<ResultService>(jsonResponse);

        return statusCode switch
        {
            HttpStatusCode.BadRequest => new ResultService
            {
                IsSuccess = false,
                Message = apiResponse?.Message
            },

            HttpStatusCode.NotFound => new ResultService
            {
                IsSuccess = false,
                Message = apiResponse?.Message
            },

            HttpStatusCode.Unauthorized => new ResultService
            {
                IsSuccess = false,
                Message = "Não autenticado! Você precisa esta autenticado para acessar esse recurso."
            },

            HttpStatusCode.Conflict => new ResultService
            {
                IsSuccess = false,
                Message = "Não é possível excluir este banco, pois ele está relacionado a outras entidades."
            },

            HttpStatusCode.Forbidden => new ResultService
            {
                IsSuccess = false,
                Message = "Não autorizado a acessar esse recurso."
            },

            // Caso haja outros status codes, pode-se adicionar aqui.
            _ => new ResultService
            {
                IsSuccess = false,
                Message = "Erro desconhecido. Tente novamente."
            }
        };
    }
    
    public static ResultService<T> ErrorResponse<T>(HttpStatusCode statusCode, string jsonResponse)
    {
        var baseError = ErrorResponse(statusCode, jsonResponse);

        return new ResultService<T>
        {
            IsSuccess = false,
            Message = baseError.Message,
            Errors = baseError.Errors,
            Data = default
        };
    }
}