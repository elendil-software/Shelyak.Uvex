using Microsoft.Extensions.Localization;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;
using Shelyak.Uvex.Web.Locales;

namespace Shelyak.Uvex.Web.Components.Shared.Alpaca;

public class AlpacaPutWithoutValueCommandHandler<T> : AlpacaCommandHandler<AlpacaPutWithoutValueCommand<T>, T>
{
    public AlpacaPutWithoutValueCommandHandler(IHttpClientFactory httpClientFactory, IStringLocalizer<LocalizationResources> localizer) : base(httpClientFactory, localizer)
    {
    }
    
    protected override async Task<HttpResponseMessage> ExecuteHttpRequest(AlpacaPutWithoutValueCommand<T> withoutValueCommand, CancellationToken ct)
    {
        return await HttpClient.PutAsJsonAsync(withoutValueCommand.Route, new AlpacaRequest(0), ct);
    }
}