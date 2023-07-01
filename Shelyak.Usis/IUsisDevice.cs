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
    IResponse<float> GetGratingAngle();
    IResponse<float> SetGratingAngle(float gratingAngle);
    IResponse<float> GetGratingWaveLength();
    IResponse<float> SetGratingWaveLength(float gratingWaveLength);
    IResponse<float> GetGratingDensity();
    IResponse<float> SetGratingDensity(float gratingDensity);
    
    IResponse<string> GetSlitId();
    IResponse<string> SetSlitId(string slitId);
    IResponse<float> GetSlitWidth();
    IResponse<float> SetSlitWidth(float slitWidth);
    IResponse<float> GetSlitAngle();
    IResponse<float> SetSlitAngle(float slitAngle);
    
    IResponse<float> GetFocusPosition();
    IResponse<float> SetFocusPosition(float focusPosition);

    IResponse<string> GetLightSource();
    IResponse<string> SetLightSource(string lightSource);
}