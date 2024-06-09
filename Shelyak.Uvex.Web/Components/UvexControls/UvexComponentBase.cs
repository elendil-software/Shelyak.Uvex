using Ardalis.Result;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Components.Shared.Toasts;
using Shelyak.Uvex.Web.Components.UvexControls.Commands;

namespace Shelyak.Uvex.Web.Components.UvexControls;

public abstract class UvexComponentBase : ComponentBase
{
    [Inject] protected IAlpacaCommands AlpacaCommands { get; set; } = null!;
    [Inject] protected ToastService ToastService { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    
    protected abstract Task LoadData();
    
    public async Task Refresh()
    {
        await LoadData();
        StateHasChanged();
    }
    
    protected async Task<Result<AlpacaResponse<T>>> ExecuteAndHandleError<T>(Func<Task<Result<AlpacaResponse<T>>>> action)
    {
        //TODO handle exceptions
        var result = await action();
        if (result.IsSuccess)
        {
            RedirectToConfigurationIfNotConnected(result.Value);
            if (result.Value.ErrorNumber != AlpacaError.NoError)
            {
                ToastService.Notify(new(ToastType.Danger, $"Error: {result.Value.ErrorMessage}."));
            }
        }
        else
        {
            ToastService.DisplayErrorsToast(result);
        }

        return result;
    }
    
    private void RedirectToConfigurationIfNotConnected<T>(AlpacaResponse<T> response)
    {
        if (response.ErrorNumber == AlpacaError.NotConnected)
        {
            NavigationManager.NavigateTo("/configuration");
        }
    }
}