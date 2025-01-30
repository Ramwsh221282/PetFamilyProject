namespace PetFamily.Api.Middleware;

public sealed record InternalExceptionDetails
{
    public int StatusCode { get; } = 500;
    public string Title { get; } = "Internal server error occured";
}

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new InternalExceptionDetails());
            _logger.LogError(ex.Message);
        }
    }
}

public static class ExceptionMiddleWareExtensions
{
    public static void UseExceptionMiddleWare(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
