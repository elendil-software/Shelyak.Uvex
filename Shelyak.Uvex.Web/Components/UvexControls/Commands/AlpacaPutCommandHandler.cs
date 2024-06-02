using Microsoft.Extensions.Localization;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;
using Shelyak.Uvex.Web.Locales;

namespace Shelyak.Uvex.Web.Components.UvexControls.Commands;

public class AlpacaPutCommandHandler<T> : AlpacaCommandHandler<AlpacaPutCommand<T>, T>
{
    public AlpacaPutCommandHandler(IHttpClientFactory httpClientFactory, IStringLocalizer<LocalizationResources> localizer) : base(httpClientFactory, localizer)
    {
    }

    protected override async Task<HttpResponseMessage> ExecuteHttpRequest(AlpacaPutCommand<T> command, CancellationToken ct)
    {
        var request = new AlpacaRequestWithValue<T>(0, command.Value);
        return await HttpClient.PutAsJsonAsync(command.Route, request, ct);
    }
}