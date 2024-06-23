using Ardalis.Result;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Components.Shared.Alpaca;
using Shelyak.Uvex.Web.Components.Shared.Toasts;

namespace Shelyak.Uvex.Web.Components.Shared.Uvex;

public abstract class UvexComponentBase : ComponentBase
{
    [Inject] protected IAlpacaCommands AlpacaCommands { get; set; } = null!;
    [Inject] protected ToastService ToastService { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    [Inject] protected IOptionsSnapshot<SerialPortSettings> SerialPortSettingsOptions { get; set; } = null!;
    
    protected abstract Task LoadData();
    
    public async Task Refresh()
    {
        await LoadData();
        StateHasChanged();
    }
    
    protected async Task<Result<AlpacaResponse<T>>> ExecuteAndHandleError<T>(Func<Task<Result<AlpacaResponse<T>>>> action)
    {
        RedirectToConfigurationIfNotConfigured();
        
        //TODO handle exceptions
        var result = await action();
        if (result.IsSuccess)
        {
            if (result.Value.ErrorNumber != AlpacaError.NoError)
            {
                ToastService.Notify(new(ToastType.Danger, result.Value.ErrorMessage));
            }
        }
        else
        {
            ToastService.DisplayErrorsToast(result);
        }

        return result;
    }

    private void RedirectToConfigurationIfNotConfigured()
    {
        if (SerialPortSettingsOptions.Value.PortName == "")
        {
            NavigationManager.NavigateTo("/configuration");
        }
    }
}