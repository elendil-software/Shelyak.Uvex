namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

public record AlpacaRequest(int DeviceNumber) : IAlpacaRequest
{
    public uint? ClientId { get; init; }
    public uint? ClientTransactionId { get; init; }
    
}