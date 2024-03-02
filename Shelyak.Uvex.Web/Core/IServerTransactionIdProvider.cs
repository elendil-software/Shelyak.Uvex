namespace Shelyak.Uvex.Web.Core;

public interface IServerTransactionIdProvider
{
    uint GetServerTransactionId();
}