using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Slit;

public class SetSlitIdEndpoint : SpectrographWithValueEndpoint<string>
{
    private readonly IUsisDevice _usisDevice;

    public SetSlitIdEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SetSlitIdEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Put(DeviceNumberRoutePattern + ApiRoutes.SlitId);
        base.Configure();
    }

    public override async Task HandleAsync(AlpacaRequestWithValue<string> request, CancellationToken ct)
    {
        UsisFunc = () => _usisDevice.SetSlitId(request.Value);
        await base.HandleAsync(request, ct);
    }
}