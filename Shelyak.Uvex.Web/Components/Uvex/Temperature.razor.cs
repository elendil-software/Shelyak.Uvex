using System;
using System.Security.Cryptography;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.HttpClients;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Shelyak.Uvex.Web.Components.Uvex;

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