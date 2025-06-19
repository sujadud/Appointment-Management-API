using System.Net;
using System.Text.Json;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errorResponse = new { message = ex.Message };
            var jsonResponse = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
