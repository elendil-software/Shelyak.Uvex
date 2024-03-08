using Shelyak.Usis.Enums;

namespace Shelyak.Uvex.Web.Components.UvexControls;

public partial class CalibrationControl : UvexComponentBase
{
    private CalibrationControlModel Model { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        await LoadCurrentLightSource();
    }
    
    protected override async Task LoadData()
    {
        await LoadCurrentLightSource();
    }

    private async Task LoadCurrentLightSource()
    {
        var currentLightSource = (await UvexHttpClient.GetLightSource());
        HandleAlpacaError(currentLightSource);
        SetSwitchesState(currentLightSource.Value.Value);
    }

    private async Task EnableSky()
    {
        await ExecuteAndHandleException(async () =>
        {
            SetSwitchesState(LightSource.SKY);
            await UvexHttpClient.SetLightSource(LightSource.SKY);
        });
    }

    private async Task EnableFlat()
    {
        await ExecuteAndHandleException(async () =>
        {
            SetSwitchesState(LightSource.FLAT);
            await UvexHttpClient.SetLightSource(LightSource.FLAT);
        });
    }

    private async Task EnableCalibration()
    {
        await ExecuteAndHandleException(async () =>
        {
            SetSwitchesState(LightSource.CALIB);
            await UvexHttpClient.SetLightSource(LightSource.CALIB);
        });
    }

    private async Task EnableDark()
    {
        await ExecuteAndHandleException(async () =>
        {
            SetSwitchesState(LightSource.DARK);
            await UvexHttpClient.SetLightSource(LightSource.DARK);
        });
    }
    
    private void SetSwitchesState(LightSource activeLightSource)
    {
        Model.Sky = activeLightSource == LightSource.SKY;
        Model.Flat = activeLightSource == LightSource.FLAT;
        Model.Calibration = activeLightSource == LightSource.CALIB;
        Model.Dark = activeLightSource == LightSource.DARK;
    }

    public class CalibrationControlModel
    {
        public bool Sky { get; set; }
        public bool Flat { get; set; }
        public bool Calibration { get; set; }
        public bool Dark { get; set; }
    }
}