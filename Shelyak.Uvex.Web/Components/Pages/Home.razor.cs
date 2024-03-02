using Shelyak.Uvex.Web.Components.Uvex;

namespace Shelyak.Uvex.Web.Components.Pages;

public partial class Home : IDisposable
{
    private Temperature TemperatureChildComponent { get; set; }
    private FocusControl FocusControlChildComponent { get; set; }
    private GratingWaveLengthControl GratingWaveLengthControlChildComponent { get; set; }
    public GratingAngleControl GratingAngleControlChildComponent { get; set; }

    protected override async Task OnInitializedAsync()
    {
        RunTimer();
    }

    private readonly PeriodicTimer _periodicTimer = new(TimeSpan.FromSeconds(2));

    private async void RunTimer()
    {
        while (await _periodicTimer.WaitForNextTickAsync())
        {
            //await LoadDataAsync();
            //await InvokeAsync(StateHasChanged);
        }
    }

    private async Task LoadDataAsync()
    {
        await TemperatureChildComponent.Refresh();
        await GratingWaveLengthControlChildComponent.Refresh();
        await GratingAngleControlChildComponent.Refresh();
        await FocusControlChildComponent.Refresh();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _periodicTimer.Dispose();
        }
    }
}