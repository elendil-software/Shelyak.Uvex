using Ardalis.Result;
using Shelyak.Usis.Enums;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.Web.Components.UvexControls.Commands;


public interface IAlpacaCommands
{
    Task<Result<AlpacaResponse<string>>> GetDeviceName();
    Task<Result<AlpacaResponse<string>>> GetSoftwareVersion();
    Task<Result<AlpacaResponse<string>>> GetProtocolVersion();
    Task<Result<AlpacaResponse<float>>> GetTemperature();
    Task<Result<AlpacaResponse<float>>> GetHumidity();
    Task<Result<AlpacaResponse<string>>> GetGratingId();
    Task<Result<AlpacaResponse<string>>> SetGratingId(string gratingId);
    Task<Result<AlpacaResponse<float>>> GetGratingAngle();
    Task<Result<AlpacaResponse<float>>> SetGratingAngle(float newPosition);
    Task<Result<AlpacaResponse<float>>> StopGratingAngle();
    Task<Result<AlpacaResponse<float>>> CalibrateGratingAngle(float gratingAngle);
    Task<Result<AlpacaResponse<float>>> GetGratingAngleMax();
    Task<Result<AlpacaResponse<float>>> GetGratingAngleMin();
    Task<Result<AlpacaResponse<float>>> GetGratingAnglePrec();
    Task<Result<AlpacaResponse<float>>> GetGratingWaveLength();
    Task<Result<AlpacaResponse<float>>> SetGratingWaveLength(float gratingWaveLength);
    Task<Result<AlpacaResponse<float>>> StopGratingWaveLength();
    Task<Result<AlpacaResponse<float>>> CalibrateGratingWaveLength(float gratingWaveLength);
    Task<Result<AlpacaResponse<float>>> GetGratingWaveLengthMax();
    Task<Result<AlpacaResponse<float>>> GetGratingWaveLengthMin();
    Task<Result<AlpacaResponse<float>>> GetGratingWaveLengthPrec();
    Task<Result<AlpacaResponse<float>>> GetGratingDensity();
    Task<Result<AlpacaResponse<float>>> SetGratingDensity(float gratingDensity);
    Task<Result<AlpacaResponse<string>>> GetSlitId();
    Task<Result<AlpacaResponse<string>>> SetSlitId(string slitId);
    Task<Result<AlpacaResponse<float>>> GetSlitWidth();
    Task<Result<AlpacaResponse<float>>> SetSlitWidth(float slitWidth);
    Task<Result<AlpacaResponse<float>>> GetSlitAngle();
    Task<Result<AlpacaResponse<float>>> SetSlitAngle(float slitAngle);
    Task<Result<AlpacaResponse<float>>> GetFocusPosition();
    Task<Result<AlpacaResponse<float>>> SetFocusPosition(float focusPosition);
    Task<Result<AlpacaResponse<float>>> StopFocusPosition();
    Task<Result<AlpacaResponse<float>>> CalibrateFocusPosition(float focusPosition);
    Task<Result<AlpacaResponse<float>>> GetFocusPositionMax();
    Task<Result<AlpacaResponse<float>>> GetFocusPositionMin();
    Task<Result<AlpacaResponse<float>>> GetFocusPositionPrec();
    Task<Result<AlpacaResponse<LightSource>>> GetLightSource();
    Task<Result<AlpacaResponse<LightSource>>> SetLightSource(LightSource lightSource);
}