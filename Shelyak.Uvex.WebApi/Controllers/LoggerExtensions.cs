namespace Shelyak.Uvex.WebApi.Controllers;

public static class LoggerExtensions
{
    public static IDisposable? BeginScope(this ILogger logger, int deviceNumber, uint clientId, uint clientTransactionId, uint serverTransactionId)
    {
        return logger.BeginScope(new Dictionary<string, object>
        {
            ["deviceNumber"] = deviceNumber,
            ["clientId"] = clientId,
            ["clientTransactionId"] = clientTransactionId,
            ["serverTransactionId"] = serverTransactionId,
        });
    }
}