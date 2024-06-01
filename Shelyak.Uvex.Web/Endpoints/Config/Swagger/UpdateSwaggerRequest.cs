namespace Shelyak.Uvex.Web.Endpoints.Config.Swagger;

public record UpdateSwaggerRequest(bool Enabled)
{
    
    public const string Route = ConfigGroup.RoutePrefix + UpdateSwaggerEndpoint.RoutePattern;
}