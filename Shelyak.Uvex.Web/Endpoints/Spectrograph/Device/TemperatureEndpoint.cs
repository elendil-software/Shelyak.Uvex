using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Device;

public class TemperatureEndpoint : SpectrographEndpoint<string>
{
    private readonly IUsisDevice _usisDevice;
    
    public TemperatureEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<TemperatureEndpoint> logger) : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }

    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.Temperature);
        base.Configure();
    }
    
    protected override Func<IResponse<string>> UsisFunc() => _usisDevice.GetTemperature;
}