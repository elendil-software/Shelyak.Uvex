using Microsoft.AspNetCore.Components;
using Shelyak.Uvex.Web.HttpClients;

namespace Shelyak.Uvex.Web.Components.Uvex;

public partial class UvexInfos
{
    [Inject]
    private IUvexHttpClient UvexHttpClient { get; set; }
    
    
    public string SpectrographName { get; set; }
    public string Firmware { get; set; }
    public string UsisProtocolVersion { get; set; }

    protected override async Task OnInitializedAsync()
    {
        SpectrographName = (await UvexHttpClient.GetDeviceName()).Value.Value;
        Firmware = (await UvexHttpClient.GetSoftwareVersion()).Value.Value;
        UsisProtocolVersion = (await UvexHttpClient.GetProtocolVersion()).Value.Value;
        
        await base.OnInitializedAsync();
    }
}