using FastEndpoints;
using FastEndpoints.Swagger;
using Shelyak.Uvex.Web.Endpoints.Shared;

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
    
    public static IApplicationBuilder UseFastEndpoints(this IApplicationBuilder app)
    {
        app.UseFastEndpoints(config =>
            {
                config.Endpoints.RoutePrefix = RoutesConst.RoutePrefix;
                config.Versioning.Prefix = RoutesConst.VersioningPrefix;
                config.Versioning.PrependToRoute = true;
                config.Errors.ResponseBuilder = ProblemDetails.ResponseBuilder;
            })
            .UseSwaggerGen();

        return app;
    }
}