using System.Net;
using System.Text.Json;
using WeCare.Domain.Utils;

namespace WeCare.Middleware;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Except ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;
            var response = new ErrorHttp(ex.StatusCode, ex.Message);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new ErrorHttp((int)HttpStatusCode.InternalServerError, ex.Message, ex.InnerException?.Message);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
