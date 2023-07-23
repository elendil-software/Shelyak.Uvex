using Shelyak.Usis.Responses;

namespace Shelyak.Usis;

public interface IUsisDevice
{
    IResponse<string> GetDeviceName();
    IResponse<string> GetSoftwareVersion();
    IResponse<string> GetProtocolVersion();
    IResponse<string> GetTemperature();
    IResponse<string> GetHumidity();

    IResponse<string> GetGratingId();
    IResponse<string> SetGratingId(string gratingId);
    IResponse<string> StopGratingId();
    IResponse<string> CalibrateGratingId(string gratingId);

    IResponse<float> GetGratingAngle();
    IResponse<float> SetGratingAngle(float gratingAngle);
    IResponse<float> StopGratingAngle();
    IResponse<float> CalibrateGratingAngle(float gratingAngle);
    IResponse<float> GetGratingAngleMax();
    IResponse<float> GetGratingAngleMin();
    IResponse<float> GetGratingAnglePrec();

    IResponse<float> GetGratingWaveLength();
    IResponse<float> SetGratingWaveLength(float gratingWaveLength);
    IResponse<float> StopGratingWaveLength();
    IResponse<float> CalibrateGratingWaveLength(float gratingWaveLength); 

    IResponse<float> GetGratingDensity();
    IResponse<float> SetGratingDensity(float gratingDensity);
    
    
    IResponse<string> GetSlitId();
    IResponse<string> SetSlitId(string slitId);
    IResponse<string> StopSlitId();
    IResponse<string> CalibrateSlitId(string slitId);

    IResponse<float> GetSlitWidth();
    IResponse<float> SetSlitWidth(float slitWidth);
    IResponse<float> StopSlitWidth();
    IResponse<float> CalibrateSlitWidth(float slitWidth);

    IResponse<float> GetSlitAngle();
    IResponse<float> SetSlitAngle(float slitAngle);
    IResponse<float> StopSlitAngle();
    IResponse<float> CalibrateSlitAngle(float slitAngle);

    IResponse<float> GetFocusPosition();
    IResponse<float> SetFocusPosition(float focusPosition);
    IResponse<float> StopFocusPosition();
    IResponse<float> CalibrateFocusPosition(float focusPosition);
    IResponse<float> GetFocusPositionMax();
    IResponse<float> GetFocusPositionMin();
    IResponse<float> GetFocusPositionPrec();

    IResponse<string> GetLightSource();
    IResponse<string> SetLightSource(string lightSource);
    IResponse<string> CalibrateLightSource(string lightSource);
    

    IResponse<string> StopAll();
}