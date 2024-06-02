namespace Shelyak.Uvex.Web.Endpoints.Config.ComPort;

public record SetComPortRequest(string PortName)
{
    public const string Route = ConfigGroup.RoutePrefix + SetComPortEndpoint.RoutePattern;
}