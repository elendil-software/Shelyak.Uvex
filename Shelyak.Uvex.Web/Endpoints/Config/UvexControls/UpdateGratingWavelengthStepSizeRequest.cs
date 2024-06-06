namespace Shelyak.Uvex.Web.Endpoints.Config.UvexControls;

public record UpdateGratingWavelengthStepSizeRequest(float StepSize)
{
    public const string Route = ConfigGroup.RoutePrefix + UpdateGratingWavelengthStepSizeEndpoint.RoutePattern;
}