using OtusSocNet.Exceptions;
using System.Net;

namespace OtusSocNet.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlingMiddleware> logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (WebApiException ex)
        {
            logger.LogError(ex, "An API exception occurred.");
            await HandleWebApiExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred.");
            await HandleGenericExceptionAsync(context);
        }
    }

    private static Task HandleWebApiExceptionAsync(HttpContext context, WebApiException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exception.StatusCode;

        var response = new { error = exception.Message };
        return context.Response.WriteAsJsonAsync(response);
    }

    private static Task HandleGenericExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new { error = "An unexpected error occurred." };
        return context.Response.WriteAsJsonAsync(response);
    }
}