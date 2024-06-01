namespace Shelyak.Uvex.Web.Endpoints.Config;

public record SetPortRequest(string PortName)
{
    public const string Route = ConfigGroup.RoutePrefix + SetPortEndpoint.RoutePattern;
}