namespace Shelyak.Uvex.Web.Endpoints.Config.UvexControls;

public record UpdateFocusStepSizeRequest(float StepSize)
{
    public const string Route = ConfigGroup.RoutePrefix + UpdateFocusStepSizeEndpoint.RoutePattern;
}