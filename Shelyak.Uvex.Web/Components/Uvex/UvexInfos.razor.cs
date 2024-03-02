using Microsoft.AspNetCore.Components;
using Shelyak.Uvex.Web.HttpClients;

namespace Shelyak.Uvex.Web.Components.Uvex;

public partial class UvexInfos : UvexComponentBase
{
    public string SpectrographName { get; set; }
    public string Firmware { get; set; }
    public string UsisProtocolVersion { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
        await base.OnInitializedAsync();
    }

    protected override async Task LoadData()
    {
        var response = await UvexHttpClient.GetDeviceName();
        RedirectToConfigurationIfNotConnected(response);
        SpectrographName = response.Value.Value;

        var softwareVersion = await UvexHttpClient.GetSoftwareVersion();
        RedirectToConfigurationIfNotConnected(softwareVersion);
        Firmware = softwareVersion.Value.Value;

        var protocolVersion = await UvexHttpClient.GetProtocolVersion();
        RedirectToConfigurationIfNotConnected(protocolVersion);
        UsisProtocolVersion = protocolVersion.Value.Value;
        
    }
}