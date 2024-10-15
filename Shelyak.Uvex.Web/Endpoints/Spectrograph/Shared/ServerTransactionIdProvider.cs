namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

public class ServerTransactionIdProvider : IServerTransactionIdProvider
{
    private uint _currentServerTransactionId;
    private readonly object _lock = new();

    public uint GetServerTransactionId()
    {
        lock (_lock)
        {
            if(_currentServerTransactionId == uint.MaxValue)
            {
                _currentServerTransactionId = 0;
            }
            _currentServerTransactionId++;
            return _currentServerTransactionId;
        }
    }
}