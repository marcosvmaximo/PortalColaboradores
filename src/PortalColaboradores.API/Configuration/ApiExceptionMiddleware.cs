using System.Net;
using System.Text.Json;

namespace PortalColaboradores.API.Configuration;

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ApiExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var apiError = new
        {
            HttpCode = (int)HttpStatusCode.InternalServerError,
            Success = false,
            Message = "Ocorreu um erro ao enviar a requisição.",
            Result = exception.Message
        };

        var result = JsonSerializer.Serialize(apiError);

        return context.Response.WriteAsync(result);
    }
}