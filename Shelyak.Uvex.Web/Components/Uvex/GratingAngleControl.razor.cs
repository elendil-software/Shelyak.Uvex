namespace Shelyak.Uvex.Web.Components.Uvex;

public partial class GratingAngleControl : UvexComponentBase
{
    public float CurrentAngle { get; set; }
    public float MinAngle { get; set; } = 0.0f;
    public float MaxAngle { get; set; } = 1000.0f;
    public AngleControlModel Model { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        MinAngle = (await UvexHttpClient.GetGratingAngleMin()).Value.Value;
        MaxAngle = (await UvexHttpClient.GetGratingAngleMax()).Value.Value;
    }
    
    protected override async Task LoadData()
    {
        var response = await UvexHttpClient.GetGratingAngle();
        HandleAlpacaError(response);
        CurrentAngle = response.Value.Value;
    }
    
    private Task ExecuteAngleControlAction()
    {
        switch (Model.Action)
        {
            case AngleControlAction.Calibrate:
                return Calibrate();
            case AngleControlAction.GoTo:
                return GoTo();
            case AngleControlAction.MoveCcw:
                return MoveIn();
            case AngleControlAction.MoveCw:
                return MoveOut();
            case AngleControlAction.Abort:
                return Abort();
            default:
                throw new InvalidOperationException($"{Model.Action} is not a supported action");
        }
    }
    
    private async Task Calibrate()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.CalibrateGratingAngle(Model.AbsolutePosition); });
    }
    
    private async Task GoTo()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.SetGratingAngle(Model.AbsolutePosition); });
    }

    private async Task MoveIn()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.SetGratingAngle(CurrentAngle - Model.StepSize); });
    }

    private async Task MoveOut()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.SetGratingAngle(CurrentAngle + Model.StepSize); });
    }
    
    private async Task Abort()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.StopGratingAngle(); });
    }


    public class AngleControlModel
    {
        public AngleControlAction Action { get; set; }
        public float StepSize { get; set; } = 0.05f;
        public float AbsolutePosition { get; set; }
    }

    public enum AngleControlAction
    {
        Calibrate,
        GoTo,
        MoveCcw,
        MoveCw,
        Abort
    }
}