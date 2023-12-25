using Shelyak.Usis.Enums;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.Web.HttpClients;

public interface IUvexHttpClient
{
    #region Device
    
    Task<AlpacaResponse<string>> GetDeviceName();
    Task<AlpacaResponse<string>> GetSoftwareVersion();
    Task<AlpacaResponse<string>> GetProtocolVersion();
    Task<AlpacaResponse<float>> GetTemperature();
    Task<AlpacaResponse<float>> GetHumidity();
    
    #endregion
    
    #region Grating
    
    Task<AlpacaResponse<string>> GetGratingId();
    Task<AlpacaResponse<string>> SetGratingId(string gratingId);
    Task<AlpacaResponse<float>> GetGratingAngle();
    Task<AlpacaResponse<float>> SetGratingAngle(float newPosition);
    Task<AlpacaResponse<float>> StopGratingAngle();
    Task<AlpacaResponse<float>> CalibrateGratingAngle(float gratingAngle);
    Task<AlpacaResponse<float>> GetGratingAngleMax();
    Task<AlpacaResponse<float>> GetGratingAngleMin();
    Task<AlpacaResponse<float>> GetGratingAnglePrec();
    
    Task<AlpacaResponse<float>> GetGratingWaveLength();
    Task<AlpacaResponse<float>> SetGratingWaveLength(float gratingWaveLength);
    Task<AlpacaResponse<float>> StopGratingWaveLength();
    Task<AlpacaResponse<float>> CalibrateGratingWaveLength(float gratingWaveLength);
    Task<AlpacaResponse<float>> GetGratingWaveLengthMax();
    Task<AlpacaResponse<float>> GetGratingWaveLengthMin();
    Task<AlpacaResponse<float>> GetGratingWaveLengthPrec();
    Task<AlpacaResponse<float>> GetGratingDensity();
    Task<AlpacaResponse<float>> SetGratingDensity(float gratingDensity);
    
    #endregion
    
    #region Slit
    
    Task<AlpacaResponse<string>> GetSlitId();
    Task<AlpacaResponse<string>> SetSlitId(string slitId);
    Task<AlpacaResponse<float>> GetSlitWidth();
    Task<AlpacaResponse<float>> SetSlitWidth(float slitWidth);
    Task<AlpacaResponse<float>> GetSlitAngle();
    Task<AlpacaResponse<float>> SetSlitAngle(float slitAngle);
    
    #endregion
    
    #region Focus
    
    Task<AlpacaResponse<float>> GetFocusPosition();
    Task<AlpacaResponse<float>> SetFocusPosition(float focusPosition);
    Task<AlpacaResponse<float>> StopFocusPosition();
    Task<AlpacaResponse<float>> CalibrateFocusPosition(float focusPosition);
    Task<AlpacaResponse<float>> GetFocusPositionMax();
    Task<AlpacaResponse<float>> GetFocusPositionMin();
    Task<AlpacaResponse<float>> GetFocusPositionPrec();

    #endregion
    
    #region Light source
    
    Task<AlpacaResponse<LightSource>> GetLightSource();
    Task<AlpacaResponse<LightSource>> SetLightSource(LightSource lightSource);
    
    #endregion
}