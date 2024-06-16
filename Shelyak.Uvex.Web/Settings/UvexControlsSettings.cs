namespace Shelyak.Uvex.Web.Settings;

public class UvexControlsSettings
{
    public const string SectionName = "UvexControls";

    public float GratingAngleStepSize { get; set; } = 5.0f;
    public float GratingWavelengthStepSize { get; set; } = 10.0f;
    public float FocusStepSize { get; set; } = 0.1f;
}