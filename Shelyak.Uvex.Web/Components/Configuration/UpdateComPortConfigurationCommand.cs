using Ardalis.Result;
using FastEndpoints;
using Microsoft.Extensions.Localization;
using Shelyak.Uvex.Web.Components.Shared.Commands;
using Shelyak.Uvex.Web.Endpoints.Config;
using Shelyak.Uvex.Web.Endpoints.Config.ComPort;
using Shelyak.Uvex.Web.Locales;

namespace Shelyak.Uvex.Web.Components.Configuration;

public record UpdateComPortConfigurationCommand(string PortName) : ICommand<Result<EmptyResult>>
{
    public class Handler : CommandHandlerBase<UpdateComPortConfigurationCommand, EmptyResult>
    {
        private readonly IStringLocalizer<LocalizationResources> _localizer;

        public Handler(IHttpClientFactory httpClientFactory, IStringLocalizer<LocalizationResources> localizer) : base(httpClientFactory)
        {
            _localizer = localizer;
        }
        
        protected override async Task<HttpResponseMessage> ExecuteHttpRequest(UpdateComPortConfigurationCommand command, CancellationToken ct)
        {
            return await HttpClient.PutAsJsonAsync(SetComPortRequest.Route, new SetComPortRequest(command.PortName), ct);
        }

        protected override Task<Result> BuildSuccessResponse(HttpResponseMessage response, CancellationToken ct)
        {
            return Task.FromResult(Result.SuccessWithMessage(_localizer["Configuration_ComPort_Command_Success"]));
        }

        protected override string GetDefaultErrorMessage()
        {
            return _localizer["Configuration_ComPort_Command_Error"];
        }
    }
}