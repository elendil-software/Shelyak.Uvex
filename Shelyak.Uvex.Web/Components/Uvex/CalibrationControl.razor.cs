using Shelyak.Usis.Enums;

namespace Shelyak.Uvex.Web.Components.Uvex;

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
        var currentLightSource = (await UvexHttpClient.GetLightSource()).Value.Value;
        Model.Sky = currentLightSource == LightSource.SKY;
        Model.Flat = currentLightSource == LightSource.FLAT;
        Model.Calibration = currentLightSource == LightSource.CALIB;
        Model.Dark = currentLightSource == LightSource.DARK;
    }

    private async Task EnableSky()
    {
        Model.Flat = false;
        Model.Calibration = false;
        Model.Dark = false;
        await UvexHttpClient.SetLightSource(LightSource.SKY);
    }

    private async Task EnableFlat()
    {
        Model.Sky = false;
        Model.Calibration = false;
        Model.Dark = false;
        await UvexHttpClient.SetLightSource(LightSource.FLAT);
    }

    private async Task EnableCalibration()
    {
        Model.Sky = false;
        Model.Flat = false;
        Model.Dark = false;
        await UvexHttpClient.SetLightSource(LightSource.CALIB);
    }

    private async Task EnableDark()
    {
        Model.Sky = false;
        Model.Flat = false;
        Model.Calibration = false;
        await UvexHttpClient.SetLightSource(LightSource.DARK);
    }

    public class CalibrationControlModel
    {
        public bool Sky { get; set; }
        public bool Flat { get; set; }
        public bool Calibration { get; set; }
        public bool Dark { get; set; }
    }
}