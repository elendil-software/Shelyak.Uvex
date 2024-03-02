using Microsoft.AspNetCore.Components;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.HttpClients;

namespace Shelyak.Uvex.Web.Components.Uvex;

public partial class FocusControl : UvexComponentBase
{
    public float MinFocusPosition { get; set; }
    public float MaxFocusPosition { get; set; }
    private float CurrentFocusPosition { get; set; }
    public FocusControlModel Model { get; set; } = new();
    

    protected override async Task OnInitializedAsync()
    {
        MinFocusPosition = (await UvexHttpClient.GetFocusPositionMin()).Value.Value;
        MaxFocusPosition = (await UvexHttpClient.GetFocusPositionMax()).Value.Value;
    }
    
    protected override async Task LoadData()
    {
        var focusPosition = await UvexHttpClient.GetFocusPosition();
        RedirectToConfigurationIfNotConnected(focusPosition);
        CurrentFocusPosition = focusPosition.Value.Value;
    }
    
    public Task ExecuteFocusControlAction()
    {
        switch (Model.Action)
        {
            case FocusControlAction.Calibrate:
                return Calibrate();
            case FocusControlAction.GoTo:
                return GoTo();
            case FocusControlAction.FocusIn:
                return FocusIn();
            case FocusControlAction.FocusOut:
                return FocusOut();
            case FocusControlAction.Abort:
                return Abort();
            default:
                throw new InvalidOperationException($"{Model.Action} is not a supported action");
        }
    }
    
    private async Task FocusOut()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.SetFocusPosition(CurrentFocusPosition + Model.StepSize); });
    }

    private async Task FocusIn()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.SetFocusPosition(CurrentFocusPosition - Model.StepSize); });
    }
    
    private async Task GoTo()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.SetFocusPosition(Model.AbsolutePosition); });
    }
    
    private async Task Abort()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.StopFocusPosition(); });
    }

    private async Task Calibrate()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.CalibrateFocusPosition(Model.AbsolutePosition); });
    }
    
    public enum FocusControlAction
    {
        Calibrate,
        GoTo,
        FocusIn,
        FocusOut,
        Abort
    }

    public class FocusControlModel
    {
        public FocusControlAction Action { get; set; }
        public float StepSize { get; set; } = 0.05f;
        public float AbsolutePosition { get; set; }
    }
}