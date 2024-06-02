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

    protected string ErrorMessage { get; set; } = "";
    
    
    protected abstract Task LoadData();
  
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
    
    public async Task Refresh()
    {
        await LoadData();
        StateHasChanged();
    }
    
    protected async Task ExecuteAndHandleError<T>(Func<Task<Result<AlpacaResponse<T>>>> action)
    {
        var result = await action();
        if (result.IsSuccess)
        {
            if (result.Value.ErrorNumber != AlpacaError.NoError)
            {
                ToastService.Notify(new(ToastType.Danger, $"Error: {result.Value.ErrorMessage}."));
            }
        }
        else
        {
            ToastService.DisplayErrorsToast(result);
        }
    }
    
    protected async Task ExecuteAndHandleException(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch(Exception ex)
        {
            ToastService.Notify(new(ToastType.Danger, $"Error: {ex.Message}."));
        }
    }

    protected void HandleAlpacaError<T>(AlpacaResponse<T> response)
    {
        RedirectToConfigurationIfNotConnected(response);
    
        if (response.ErrorNumber == AlpacaError.NoError)
        {
            ErrorMessage = "";
            return;
        }
        
        ErrorMessage = response.ErrorMessage;
    }
    
    protected void RedirectToConfigurationIfNotConnected<T>(AlpacaResponse<T> response)
    {
        if (response.ErrorNumber == AlpacaError.NotConnected)
        {
            NavigationManager.NavigateTo("/configuration");
        }
    }
}