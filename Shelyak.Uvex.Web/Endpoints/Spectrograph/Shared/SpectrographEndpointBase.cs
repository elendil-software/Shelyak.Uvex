using FastEndpoints;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

public abstract class SpectrographEndpointBase<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull
{
    protected const string DeviceNumberRoutePattern = "/{deviceNumber}/";
    private readonly IServerTransactionIdProvider _serverTransactionIdProvider;
    private readonly ILogger<SpectrographEndpointBase<TRequest, TResponse>> _logger;

    protected SpectrographEndpointBase(IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SpectrographEndpointBase<TRequest, TResponse>> logger)
    {
        _serverTransactionIdProvider = serverTransactionIdProvider;
        _logger = logger;
    }
    
    public override void Configure()
    {
        Group<SpectrographGroup>();
        Version(1);
        AllowAnonymous();
    }

    protected AlpacaResponse<T> Execute<T>(Func<IResponse<T>> usisCommand, IAlpacaRequest request/*int deviceNumber, uint? clientId, uint? clientTransactionId*/)
    {
        uint serverTransactionId = _serverTransactionIdProvider.GetServerTransactionId();
        using IDisposable? scope = _logger.BeginScope(request.DeviceNumber, request.ClientId, request.ClientTransactionId, serverTransactionId);
        
        try
        {
            IResponse<T> response = usisCommand();
            AlpacaResponse<T> alpacaResponse = AlpacaResponseBuilder.BuildAlpacaResponse(request.ClientTransactionId, serverTransactionId, response);
            return alpacaResponse;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing USIS command : {ExceptionMessage}", e.Message);
            return AlpacaResponseBuilder.BuildAlpacaResponse<T>(request.ClientTransactionId, serverTransactionId, e);
        }
    }
}