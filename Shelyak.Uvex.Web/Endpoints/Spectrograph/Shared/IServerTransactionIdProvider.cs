namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

public interface IServerTransactionIdProvider
{
    uint GetServerTransactionId();
}