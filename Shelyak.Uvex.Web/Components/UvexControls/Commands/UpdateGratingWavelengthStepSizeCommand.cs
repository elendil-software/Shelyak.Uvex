using Ardalis.Result;
using FastEndpoints;
using Microsoft.Extensions.Localization;
using Shelyak.Uvex.Web.Components.Shared.Commands;
using Shelyak.Uvex.Web.Endpoints.Config.UvexControls;
using Shelyak.Uvex.Web.Locales;

namespace Shelyak.Uvex.Web.Components.UvexControls.Commands;

public record UpdateGratingWavelengthStepSizeCommand(float StepSize) : ICommand<Result<EmptyResult>>
{
    public class Handler : CommandHandlerBase<UpdateGratingWavelengthStepSizeCommand, EmptyResult>
    {
        private readonly IStringLocalizer<LocalizationResources> _localizer;

        public Handler(IHttpClientFactory httpClientFactory, IStringLocalizer<LocalizationResources> localizer) : base(httpClientFactory)
        {
            _localizer = localizer;
        }

        protected override async Task<HttpResponseMessage> ExecuteHttpRequest(UpdateGratingWavelengthStepSizeCommand command, CancellationToken ct)
        {
            return await HttpClient.PutAsJsonAsync(UpdateGratingWavelengthStepSizeRequest.Route, new UpdateGratingAngleStepSizeRequest(command.StepSize), ct);
        }

        protected override Task<Result<EmptyResult>> BuildSuccessResponse(HttpResponseMessage response, CancellationToken ct)
        {
            return Task.FromResult(Result.Success(EmptyResult.Instance));
        }

        protected override string GetDefaultErrorMessage()
        {
            return _localizer["Configuration_Focus_Config_StepSize_Command_Error"];
        }
    }
}