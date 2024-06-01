using Ardalis.Result;
using FastEndpoints;
using Shelyak.Uvex.Web.Components.Shared.Commands;
using Shelyak.Uvex.Web.Endpoints.Config.Swagger;

namespace Shelyak.Uvex.Web.Components.Configuration;

public record UpdateSwaggerConfigurationCommand(bool Enabled) : ICommand<Result<EmptyResult>>
{
    public class Handler : CommandHandlerBase<UpdateSwaggerConfigurationCommand, EmptyResult>
    {
        public Handler(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
        
        protected override async Task<HttpResponseMessage> ExecuteHttpRequest(UpdateSwaggerConfigurationCommand command, CancellationToken ct)
        {
            return await HttpClient.PutAsJsonAsync(UpdateSwaggerRequest.Route, new UpdateSwaggerRequest(command.Enabled), ct);
        }

        protected override Task<Result> BuildSuccessResponse(HttpResponseMessage response, CancellationToken ct)
        {
            return Task.FromResult(Result.SuccessWithMessage("Swagger configuration updated successfully"));
        }

        protected override string GetDefaultErrorMessage()
        {
            return "An error occurred while updating the swagger configuration";
        }
    }
}
