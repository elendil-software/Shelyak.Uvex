using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Slit;

public class SlitIdEndpoint : SpectrographEndpoint<string>
{
    private readonly IUsisDevice _usisDevice;

    public SlitIdEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SlitIdEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.SlitId);
        base.Configure();
    }

    protected override Func<IResponse<string>> UsisFunc() => _usisDevice.GetSlitId;
}