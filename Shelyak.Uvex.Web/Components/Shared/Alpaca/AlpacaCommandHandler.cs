using Ardalis.Result;
using FastEndpoints;
using Microsoft.Extensions.Localization;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Components.Shared.Commands;
using Shelyak.Uvex.Web.Configuration;
using Shelyak.Uvex.Web.Locales;

namespace Shelyak.Uvex.Web.Components.Shared.Alpaca;

public abstract class AlpacaCommandHandler<TCommand, T> : CommandHandlerBase<TCommand, AlpacaResponse<T>> where TCommand : ICommand<Result<AlpacaResponse<T>>>
{
    private readonly IStringLocalizer<LocalizationResources> _localizer;

    protected AlpacaCommandHandler(IHttpClientFactory httpClientFactory, IStringLocalizer<LocalizationResources> localizer) : base(httpClientFactory, HttpClientConst.ApiSpectrographHttpClient)
    {
        _localizer = localizer;
    }
    
    protected override async Task<Result<AlpacaResponse<T>>> BuildSuccessResponse(HttpResponseMessage response, CancellationToken ct)
    {
        AlpacaResponse<T> result = (await response.Content.ReadFromJsonAsync<AlpacaResponse<T>>(cancellationToken: ct))!;
        return Result.Success(result);
    }

    protected override string GetDefaultErrorMessage()
    {
        return _localizer["UvexControl_Command_Error"];
    }
}