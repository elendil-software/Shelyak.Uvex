namespace Shelyak.Uvex.Web;

public class ServerTransactionIdProvider : IServerTransactionIdProvider
{
    private uint _currentServerTransactionId = 0;
    private readonly object _lock = new();

    public uint GetServerTransactionId()
    {
        lock (_lock)
        {
            _currentServerTransactionId++;
            return _currentServerTransactionId;
        }
    }
}