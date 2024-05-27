using FastEndpoints;
using FastEndpoints.Swagger;
using Shelyak.Uvex.Web.Endpoints.Shared;
using Shelyak.Uvex.Web.Settings;

namespace Shelyak.Uvex.Web.Configuration;

internal static class FastEndpointsConfigurationExtensions
{
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
                config.Endpoints.RoutePrefix = RoutesConst.RoutePrefix;
                config.Versioning.Prefix = RoutesConst.VersioningPrefix;
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
}