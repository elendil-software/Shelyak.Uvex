﻿using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Grating;

public class CalibrateGratingWaveLengthEndpoint : SpectrographWithValueEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public CalibrateGratingWaveLengthEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<CalibrateGratingWaveLengthEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Put(DeviceNumberRoutePattern + ApiRoutes.CalibrateGratingWaveLength);
        base.Configure();
    }
    
    public override async Task HandleAsync(AlpacaRequestWithValue<float> request, CancellationToken ct)
    {
        UsisFunc = () => _usisDevice.CalibrateGratingWaveLength(request.Value);
        await base.HandleAsync(request, ct);
    }
}