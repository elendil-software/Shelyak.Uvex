using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.HttpClients;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Shelyak.Uvex.Web.Components.Uvex;

public partial class Temperature
{
    [Inject]
    private IUvexHttpClient UvexHttpClient { get; set; }
    
    public float CurrentTemperature { get; set; }
  
    protected override async Task OnInitializedAsync()
    {
        CurrentTemperature = (await UvexHttpClient.GetTemperature()).Value.Value;
        await base.OnInitializedAsync();
    }
}