using FastEndpoints;
using FastEndpoints.Swagger;
using Shelyak.Uvex.Web.Settings;

namespace Shelyak.Uvex.Web.Configuration;

internal static class FastEndpointsConfigurationExtensions
{
    private const string RoutePrefix = "api";
    private const string VersioningPrefix = "v";
    
    public static IHostApplicationBuilder AddFastEndpoints(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddFastEndpoints()
            .SwaggerDocument(o =>
            {
                o.MaxEndpointVersion = 1;
                o.DocumentSettings = s =>
                {
                    s.DocumentName = "USIS Device API";
                    s.Title = "USIS Device API";
                    s.Version = "v1";
                };
            });

        return builder;
    }
    
    public static IApplicationBuilder UseFastEndpoints(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseFastEndpoints(config =>
            {
                config.Endpoints.RoutePrefix = RoutePrefix;
                config.Versioning.Prefix = VersioningPrefix;
                config.Versioning.PrependToRoute = true;
                config.Errors.ResponseBuilder = ProblemDetails.ResponseBuilder;
            });
        
        if (IsSwaggerEnabled(configuration))
        {
            app.UseSwaggerGen();
        }

        return app;
    }
    
    private static bool IsSwaggerEnabled(IConfiguration configuration)
    {
        SwaggerSettings? swaggerSettings = configuration.GetSection(SwaggerSettings.SectionName).Get<SwaggerSettings>();
        return swaggerSettings?.Enabled ?? false;
    }

    public static string GetApiBasePath(int version = 1)
    {
        return $"/{RoutePrefix}/{VersioningPrefix}{version}/";
    }
}