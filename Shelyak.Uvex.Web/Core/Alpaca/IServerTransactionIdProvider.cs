namespace Shelyak.Uvex.Web.Core.Alpaca;

public interface IServerTransactionIdProvider
{
    uint GetServerTransactionId();
}