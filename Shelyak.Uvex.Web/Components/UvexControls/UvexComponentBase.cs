using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.HttpClients;

namespace Shelyak.Uvex.Web.Components.UvexControls;

public abstract class UvexComponentBase : ComponentBase
{
    [Inject] protected IUvexHttpClient UvexHttpClient { get; set; }
    [Inject] protected ToastService ToastService { get; set; }
    [Inject] protected NavigationManager NavigationManager { get; set; }

    public string ErrorMessage { get; set; } = "";
    
    
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