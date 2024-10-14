using FastEndpoints;

namespace Shelyak.Uvex.Web.Endpoints.Config.UvexControls;

public class UpdateFocusStepSizeEndpoint : Endpoint<UpdateFocusStepSizeRequest>
{
    public const string RoutePattern = "/UvexControls/Focus/StepSize";
    
    private readonly ISettingsUpdater _settingsUpdater;
    
    public UpdateFocusStepSizeEndpoint(ISettingsUpdater settingsUpdater)
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
    
    public override async Task HandleAsync(UpdateFocusStepSizeRequest req, CancellationToken ct)
    {
        await _settingsUpdater.UpdateFocusStepSize(req.StepSize);
        await SendOkAsync(ct);
    }
}