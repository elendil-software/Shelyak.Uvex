using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Grating;

public class SetGratingIdEndpoint : SpectrographWithValueEndpoint<string>
{
    private readonly IUsisDevice _usisDevice;

    public SetGratingIdEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SetGratingIdEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Put(DeviceNumberRoutePattern + ApiRoutes.GratingId);
        base.Configure();
    }
    
    public override async Task HandleAsync(AlpacaRequestWithValue<string> request, CancellationToken ct)
    {
        UsisFunc = () => _usisDevice.SetGratingId(request.Value);
        await base.HandleAsync(request, ct);
    }
}