using FastEndpoints;

namespace Shelyak.Uvex.Web.Endpoints.Config.UvexControls;

public class UpdateGratingWavelengthStepSizeEndpoint : Endpoint<UpdateGratingWavelengthStepSizeRequest>
{
    public const string RoutePattern = "/UvexControls/GratingWavelength/StepSize";
    
    private readonly ISettingsUpdater _settingsUpdater;
    
    public UpdateGratingWavelengthStepSizeEndpoint(ISettingsUpdater settingsUpdater)
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

    public override async Task HandleAsync(UpdateGratingWavelengthStepSizeRequest req, CancellationToken ct)
    {
        await _settingsUpdater.UpdateGratingWavelengthStepSize(req.StepSize);
        await SendOkAsync(ct);
    }
}