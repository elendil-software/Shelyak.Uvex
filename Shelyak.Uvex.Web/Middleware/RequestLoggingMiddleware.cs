using System.Text;

namespace Shelyak.Uvex.Web.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();

        // Request logging
        var builder = new StringBuilder();
        var request = context.Request;
        builder.AppendLine("-------Request-------");
        builder.AppendLine($"{request.Method} {request.Scheme}://{request.Host}{request.Path} {request.QueryString.Value}");
        builder.AppendLine("Headers:");
        foreach (var (key, value) in request.Headers)
        {
            builder.AppendLine($"\t{key}: {value}");
        }
        
        if (request.Method != HttpMethods.Get)
        {
            request.Body.Position = 0;
            using var reader = new StreamReader(request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            builder.AppendLine("Body:");
            builder.AppendLine(body);
            request.Body.Position = 0;
        }

        _logger.LogInformation(builder.ToString());

        builder.Clear();

        await _next(context);
    }
}