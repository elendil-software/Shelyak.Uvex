using Ardalis.Result;
using FastEndpoints;
using Shelyak.Usis.Enums;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.Web.Components.UvexControls.Commands;


public class AlpacaCommands : IAlpacaCommands
{
    #region Device
    
    public Task<Result<AlpacaResponse<string>>> GetDeviceName() => new AlpacaGetCommand<string>(ApiRoutes.DeviceName).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<string>>> GetSoftwareVersion() => new AlpacaGetCommand<string>(ApiRoutes.SoftwareVersion).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<string>>> GetProtocolVersion() => new AlpacaGetCommand<string>(ApiRoutes.ProtocolVersion).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetTemperature() => new AlpacaGetCommand<float>(ApiRoutes.Temperature).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetHumidity() => new AlpacaGetCommand<float>(ApiRoutes.Humidity).ExecuteAsync();
    
    #endregion
    
    #region Grating
    
    public Task<Result<AlpacaResponse<string>>> GetGratingId() => new AlpacaGetCommand<string>(ApiRoutes.GratingId).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<string>>> SetGratingId(string gratingId) => new AlpacaPutCommand<string>(gratingId, ApiRoutes.GratingId).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetGratingAngle() => new AlpacaGetCommand<float>(ApiRoutes.GratingAngle).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> SetGratingAngle(float newPosition) => new AlpacaPutCommand<float>(newPosition, ApiRoutes.GratingAngle).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> StopGratingAngle() => new AlpacaPutWithoutValueCommand<float>(ApiRoutes.StopGratingAngle).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> CalibrateGratingAngle(float gratingAngle) => new AlpacaPutCommand<float>(gratingAngle, ApiRoutes.CalibrateGratingAngle).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetGratingAngleMax() => new AlpacaGetCommand<float>(ApiRoutes.GratingAngleMax).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetGratingAngleMin() => new AlpacaGetCommand<float>(ApiRoutes.GratingAngleMin).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetGratingAnglePrec() => new AlpacaGetCommand<float>(ApiRoutes.GratingAnglePrec).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetGratingWaveLength() => new AlpacaGetCommand<float>(ApiRoutes.GratingWaveLength).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> SetGratingWaveLength(float gratingWaveLength) => new AlpacaPutCommand<float>(gratingWaveLength, ApiRoutes.GratingWaveLength).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> StopGratingWaveLength() => new AlpacaPutWithoutValueCommand<float>(ApiRoutes.StopGratingWaveLength).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> CalibrateGratingWaveLength(float gratingWaveLength) => new AlpacaPutCommand<float>(gratingWaveLength, ApiRoutes.CalibrateGratingWaveLength).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetGratingWaveLengthMax() => new AlpacaGetCommand<float>(ApiRoutes.GratingWaveLengthMax).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetGratingWaveLengthMin() => new AlpacaGetCommand<float>(ApiRoutes.GratingWaveLengthMin).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetGratingWaveLengthPrec() => new AlpacaGetCommand<float>(ApiRoutes.GratingWaveLengthPrec).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetGratingDensity() => new AlpacaGetCommand<float>(ApiRoutes.GratingDensity).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> SetGratingDensity(float gratingDensity) => new AlpacaPutCommand<float>(gratingDensity, ApiRoutes.SlitAngle).ExecuteAsync();
    
    #endregion
    
    #region Slit
    
    public Task<Result<AlpacaResponse<string>>> GetSlitId() => new AlpacaGetCommand<string>(ApiRoutes.SlitId).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<string>>> SetSlitId(string slitId) => new AlpacaGetCommand<string>(ApiRoutes.SlitId).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetSlitWidth() => new AlpacaGetCommand<float>(ApiRoutes.SlitWidth).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> SetSlitWidth(float slitWidth) => new AlpacaPutCommand<float>(slitWidth, ApiRoutes.SlitWidth).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetSlitAngle() => new AlpacaGetCommand<float>(ApiRoutes.SlitAngle).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> SetSlitAngle(float slitAngle) => new AlpacaPutCommand<float>(slitAngle, ApiRoutes.SlitAngle).ExecuteAsync();
    
    #endregion
    
    #region Focus
    public Task<Result<AlpacaResponse<float>>> GetFocusPosition() => new AlpacaGetCommand<float>(ApiRoutes.FocusPosition).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> SetFocusPosition(float focusPosition) => new AlpacaPutCommand<float>(focusPosition, ApiRoutes.FocusPosition).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> StopFocusPosition() => new AlpacaPutWithoutValueCommand<float>(ApiRoutes.StopFocusPosition).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> CalibrateFocusPosition(float focusPosition) => new AlpacaPutCommand<float>(focusPosition, ApiRoutes.CalibrateFocusPosition).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetFocusPositionMax() => new AlpacaGetCommand<float>(ApiRoutes.FocusPositionMax).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetFocusPositionMin() => new AlpacaGetCommand<float>(ApiRoutes.FocusPositionMin).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<float>>> GetFocusPositionPrec() => new AlpacaGetCommand<float>(ApiRoutes.FocusPositionPrec).ExecuteAsync();

    #endregion
    
    #region Light source
    
    public Task<Result<AlpacaResponse<LightSource>>> GetLightSource() => new AlpacaGetCommand<LightSource>(ApiRoutes.LightSource).ExecuteAsync();
    
    public Task<Result<AlpacaResponse<LightSource>>> SetLightSource(LightSource lightSource) => new AlpacaPutCommand<LightSource>(lightSource, ApiRoutes.LightSource).ExecuteAsync();
    
    #endregion
}