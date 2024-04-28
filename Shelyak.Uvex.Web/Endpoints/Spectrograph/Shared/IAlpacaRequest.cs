namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

public interface IAlpacaRequest
{
    public int DeviceNumber { get; }
    public uint? ClientId { get; }
    public uint? ClientTransactionId { get; }
}