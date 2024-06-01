using System.Text;
using System.Text.Json;
using Ardalis.Result;
using FastEndpoints;
using Shelyak.Uvex.Web.Configuration;

namespace Shelyak.Uvex.Web.Components.Shared.Commands;

public abstract class CommandHandlerBase<TCommand, TResult> : ICommandHandler<TCommand, Result<TResult>> where TCommand : ICommand<Result<TResult>>
{
    protected readonly HttpClient HttpClient;

    protected CommandHandlerBase(IHttpClientFactory httpClientFactory)
    {
        HttpClient = httpClientFactory.CreateClient(HttpClientConst.ApiConfigHttpClient);
    }
    
    protected abstract Task<HttpResponseMessage> ExecuteHttpRequest(TCommand command, CancellationToken ct);
    protected abstract Task<Result> BuildSuccessResponse(HttpResponseMessage response, CancellationToken ct);
    protected abstract string GetDefaultErrorMessage();
    
    
    public virtual async Task<Result<TResult>> ExecuteAsync(TCommand command, CancellationToken ct)
    {
        HttpResponseMessage response = await ExecuteHttpRequest(command, ct);

        if (response.IsSuccessStatusCode)
        {
            return await BuildSuccessResponse(response, ct);
        }

        var mediaType = response.Content.Headers.ContentType?.MediaType;
        if (mediaType != null && mediaType.Equals("application/problem+json", StringComparison.InvariantCultureIgnoreCase))
        {
            return await GetMessageFromProblemDetails(response, ct);
        }
        
        return Result.Error(GetDefaultErrorMessage());
    }
    
    private async Task<Result<TResult>> GetMessageFromProblemDetails(HttpResponseMessage response, CancellationToken ct)
    {
        var problemDetails = await response.Content.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails>((JsonSerializerOptions)null, ct) ?? new Microsoft.AspNetCore.Mvc.ProblemDetails();

        var error = new StringBuilder();
        if (!string.IsNullOrEmpty(problemDetails.Title))
        {
            error.AppendLine(problemDetails.Title);
        }
        
        if (!string.IsNullOrEmpty(problemDetails.Detail))
        {
            error.AppendLine(problemDetails.Detail);
        }
        
        if(error.Length == 0)
        {
            error.AppendLine(GetDefaultErrorMessage());
        }
        
        return Result.Error(error.ToString());
    }
}