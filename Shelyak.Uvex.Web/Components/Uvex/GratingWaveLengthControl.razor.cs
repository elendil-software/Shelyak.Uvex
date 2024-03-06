﻿namespace Shelyak.Uvex.Web.Components.Uvex;

public partial class GratingWaveLengthControl : UvexComponentBase
{
    public float CurrentWavelength { get; set; }
    public float MinWavelength { get; set; } = 0.0f;
    public float MaxWavelength { get; set; } = 1000.0f;
    public WavelengthControlModel Model { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        var gratingWaveLengthMin = await UvexHttpClient.GetGratingWaveLengthMin();
        HandleAlpacaError(gratingWaveLengthMin);
        MinWavelength = gratingWaveLengthMin.Value.Value;

        var gratingWaveLengthMax = await UvexHttpClient.GetGratingWaveLengthMax();
        HandleAlpacaError(gratingWaveLengthMax);
        MinWavelength = gratingWaveLengthMax.Value.Value;
    }
    
    protected override async Task LoadData()
    {
        var response = await UvexHttpClient.GetGratingWaveLength();
        HandleAlpacaError(response);
        CurrentWavelength = response.Value.Value;
    }
    
    private Task ExecuteWavelengthControlAction()
    {
        switch (Model.Action)
        {
            case WavelengthControlAction.GoTo:
                return GoTo();
            case WavelengthControlAction.MoveIn:
                return MoveIn();
            case WavelengthControlAction.MoveOut:
                return MoveOut();
            case WavelengthControlAction.Abort:
                return Abort();
            default:
                throw new InvalidOperationException($"{Model.Action} is not a supported action");
        }
    }
    
    private async Task GoTo()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.SetGratingWaveLength(Model.AbsolutePosition); });
    }

    private async Task MoveIn()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.SetGratingWaveLength(CurrentWavelength - Model.StepSize); });
    }

    private async Task MoveOut()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.SetGratingWaveLength(CurrentWavelength + Model.StepSize); });
    }
    
    private async Task Abort()
    {
        await ExecuteAndHandleException(async () => { await UvexHttpClient.StopGratingWaveLength(); });
    }
    
    public class WavelengthControlModel
    {
        public WavelengthControlAction Action { get; set; }
        public float StepSize { get; set; } = 0.05f;
        public float AbsolutePosition { get; set; }
    }

    public enum WavelengthControlAction
    {
        GoTo,
        MoveIn,
        MoveOut,
        Abort
    }
}