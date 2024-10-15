using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

public abstract class SpectrographWithValueEndpoint<T> : SpectrographEndpointBase<AlpacaRequestWithValue<T>, AlpacaResponse<T>>
{
    protected SpectrographWithValueEndpoint(IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SpectrographWithValueEndpoint<T>> logger)
        : base(serverTransactionIdProvider, logger)
    {
    }
    
    protected Func<IResponse<T>> UsisFunc { get; set; } = () => throw new InvalidOperationException("USIS function not set.");
    
    public override async Task HandleAsync(AlpacaRequestWithValue<T> request, CancellationToken ct)
    {
        var result = Execute(UsisFunc, request);
        await SendAsync(result, cancellation: ct);
    }
}