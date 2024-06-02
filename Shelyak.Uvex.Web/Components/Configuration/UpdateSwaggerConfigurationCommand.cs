using Ardalis.Result;
using FastEndpoints;
using Microsoft.Extensions.Localization;
using Shelyak.Uvex.Web.Components.Shared.Commands;
using Shelyak.Uvex.Web.Endpoints.Config.Swagger;
using Shelyak.Uvex.Web.Locales;

namespace Shelyak.Uvex.Web.Components.Configuration;

public record UpdateSwaggerConfigurationCommand(bool Enabled) : ICommand<Result<EmptyResult>>
{
    public class Handler : CommandHandlerBase<UpdateSwaggerConfigurationCommand, EmptyResult>
    {
        private readonly IStringLocalizer<LocalizationResources> _localizer;

        public Handler(IHttpClientFactory httpClientFactory, IStringLocalizer<LocalizationResources> localizer) : base(httpClientFactory)
        {
            _localizer = localizer;
        }
        
        protected override async Task<HttpResponseMessage> ExecuteHttpRequest(UpdateSwaggerConfigurationCommand command, CancellationToken ct)
        {
            return await HttpClient.PutAsJsonAsync(UpdateSwaggerRequest.Route, new UpdateSwaggerRequest(command.Enabled), ct);
        }

        protected override Task<Result<EmptyResult>> BuildSuccessResponse(HttpResponseMessage response, CancellationToken ct)
        {
            return Task.FromResult(Result.Success(EmptyResult.Instance, _localizer["Configuration_Swagger_Command_Success"]));
        }

        protected override string GetDefaultErrorMessage()
        {
            return _localizer["Configuration_Swagger_Command_Error"];
        }
    }
}
