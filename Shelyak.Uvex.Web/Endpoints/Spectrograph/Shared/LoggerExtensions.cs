namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

public static class LoggerExtensions
{
    public static IDisposable? BeginScope(this ILogger logger, int deviceNumber, uint? clientId, uint? clientTransactionId, uint serverTransactionId)
    {
        return logger.BeginScope(new Dictionary<string, object>
        {
            ["deviceNumber"] = deviceNumber,
            ["clientId"] = clientId ?? 0,
            ["clientTransactionId"] = clientTransactionId ?? 0,
            ["serverTransactionId"] = serverTransactionId,
        });
    }
}