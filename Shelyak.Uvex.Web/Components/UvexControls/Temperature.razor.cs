namespace Shelyak.Uvex.Web.Components.UvexControls;

public partial class Temperature : UvexComponentBase
{
    
    
    public float CurrentTemperature { get; set; }
  
    protected override async Task OnInitializedAsync()
    {
        await LoadData();
        await base.OnInitializedAsync();
    }
    
    protected override async Task LoadData()
    {
        var response = await UvexHttpClient.GetTemperature();
        HandleAlpacaError(response);
        CurrentTemperature = response.Value.Value;
    }
}