using FastEndpoints;
using Shelyak.Uvex.Web.Core.Settings;

namespace Shelyak.Uvex.Web.Endpoints.Config.UvexControls;

public class UpdateGratingAngleStepSizeEndpoint : Endpoint<UpdateGratingAngleStepSizeRequest>
{
    public const string RoutePattern = "/UvexControls/GratingAngle/StepSize";
    
    private readonly ISettingsUpdater _settingsUpdater;

    public UpdateGratingAngleStepSizeEndpoint(ISettingsUpdater settingsUpdater)
    {
        _settingsUpdater = settingsUpdater;
    }
    
    public override void Configure()
    {
        Put(RoutePattern);
        Group<ConfigGroup>();
        Version(1);
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateGratingAngleStepSizeRequest req, CancellationToken ct)
    {
        await _settingsUpdater.UpdateGratingAngleStepSize(req.StepSize);
        await SendOkAsync(ct);
    }
}