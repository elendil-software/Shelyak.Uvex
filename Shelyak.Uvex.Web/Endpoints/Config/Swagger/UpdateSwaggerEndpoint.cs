using FastEndpoints;
using Shelyak.Uvex.Web.Core.Settings;

namespace Shelyak.Uvex.Web.Endpoints.Config.Swagger;

public class UpdateSwaggerEndpoint : Endpoint<UpdateSwaggerRequest>
{
    internal const string RoutePattern = "/swagger";
        
    private readonly ISettingsUpdater _settingsUpdater;

    public UpdateSwaggerEndpoint(ISettingsUpdater settingsUpdater)
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

    public override async Task HandleAsync(UpdateSwaggerRequest req, CancellationToken ct)
    {
        await _settingsUpdater.UpdateSwagger(req.Enabled);
        await SendOkAsync(ct);
    }
}