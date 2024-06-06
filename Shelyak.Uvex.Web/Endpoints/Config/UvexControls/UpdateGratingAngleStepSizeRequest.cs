namespace Shelyak.Uvex.Web.Endpoints.Config.UvexControls;

public record UpdateGratingAngleStepSizeRequest(float StepSize)
{
    public const string Route = ConfigGroup.RoutePrefix + UpdateGratingAngleStepSizeEndpoint.RoutePattern;
}