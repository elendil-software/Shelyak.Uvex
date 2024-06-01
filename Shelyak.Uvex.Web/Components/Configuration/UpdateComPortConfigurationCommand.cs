using Ardalis.Result;
using FastEndpoints;
using Shelyak.Uvex.Web.Components.Shared.Commands;
using Shelyak.Uvex.Web.Endpoints.Config;

namespace Shelyak.Uvex.Web.Components.Configuration;

public record UpdateComPortConfigurationCommand(string PortName) : ICommand<Result<EmptyResult>>
{
    public class Handler : CommandHandlerBase<UpdateComPortConfigurationCommand, EmptyResult>
    {
        public Handler(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
        
        protected override async Task<HttpResponseMessage> ExecuteHttpRequest(UpdateComPortConfigurationCommand command, CancellationToken ct)
        {
            return await HttpClient.PutAsJsonAsync(SetPortRequest.Route, new SetPortRequest(command.PortName), ct);
        }

        protected override Task<Result> BuildSuccessResponse(HttpResponseMessage response, CancellationToken ct)
        {
            return Task.FromResult(Result.SuccessWithMessage("COM port configuration updated successfully"));
        }

        protected override string GetDefaultErrorMessage()
        {
            return "An error occurred while updating the COM port configuration";
        }
    }
}