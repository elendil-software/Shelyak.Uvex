using FastEndpoints;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

public record AlpacaRequestWithValue<T>(int DeviceNumber, T Value) : IAlpacaRequest
{
    [QueryParam]
    public uint? ClientId { get; init;}
    [QueryParam]
    public uint? ClientTransactionId { get; init; }
}