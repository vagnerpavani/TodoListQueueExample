using System.Net;
using firstproject.Exceptions;

namespace firstproject.Middlewares;


public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError("\n \n \n" + exception, "An unexpected error occurred. \n \n \n");

        ExceptionResponse response = exception switch
        {
            EmailAlreadyInUseException _ => new ExceptionResponse(HttpStatusCode.Conflict, exception.Message),
            InvalidCredentialsException _ => new ExceptionResponse(HttpStatusCode.Unauthorized, exception.Message),
            _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);

