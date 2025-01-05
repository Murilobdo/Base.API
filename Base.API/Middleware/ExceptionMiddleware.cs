using System.Net;
using System.Text;
using System.Text.Json;
using Base.API.ViewModels;
using Base.Core.Interfaces.Cache;

namespace Base.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly ICacheService _cacheService;
    
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, ICacheService cacheService)
    {
        _next = next;
        _logger = logger;
        _cacheService = cacheService;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = GetFullMessageError(ex);
            string key = $"api-exception:{DateTime.Now.ToString("g").Replace(":", "-")}";
            await _cacheService.SetAsync(key, response);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {

        var statusCode = HttpStatusCode.InternalServerError;

        if (exception is KeyNotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
        }

        var response = GetFullMessageError(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var jsonResponse = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }
    
    public static ExceptionViewModel GetFullMessageError(Exception ex)
    {
        return new ExceptionViewModel
        {
            Title = "Classe: ExceptionMiddleware",
            Message = ex.Message,
            StackTrace = ex.StackTrace,
            CreateAt = DateTime.Now,
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Detail = ex.InnerException?.Message
        };
    }
}