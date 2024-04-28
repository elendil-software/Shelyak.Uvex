using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

public abstract class SpectrographEndpoint<T> : SpectrographEndpointBase<AlpacaRequest, AlpacaResponse<T>>
{
    protected SpectrographEndpoint(IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SpectrographEndpoint<T>> logger)
        : base(serverTransactionIdProvider, logger)
    {
    }
    
    protected abstract Func<IResponse<T>> UsisFunc();
    
    public override async Task HandleAsync(AlpacaRequest request, CancellationToken ct)
    {
        AlpacaResponse<T> result = Execute(UsisFunc(), request);
        await SendAsync(result, cancellation: ct);
    }
}