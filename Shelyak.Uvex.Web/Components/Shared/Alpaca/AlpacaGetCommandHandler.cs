using Microsoft.Extensions.Localization;
using Shelyak.Uvex.Web.Locales;

namespace Shelyak.Uvex.Web.Components.Shared.Alpaca;

public class AlpacaGetCommandHandler<T> : AlpacaCommandHandler<AlpacaGetCommand<T>, T>
{
    public AlpacaGetCommandHandler(IHttpClientFactory httpClientFactory, IStringLocalizer<LocalizationResources> localizer) : base(httpClientFactory, localizer)
    {
    }

    protected override async Task<HttpResponseMessage> ExecuteHttpRequest(AlpacaGetCommand<T> command, CancellationToken ct)
    {
        return await HttpClient.GetAsync(command.Route, ct);
    }
}