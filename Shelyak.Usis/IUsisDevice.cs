using Shelyak.Usis.Responses;

namespace Shelyak.Usis;

public interface IUsisDevice
{
    IResponse GetDeviceName();
    IResponse GetSoftwareVersion();
    IResponse GetProtocolVersion();
    IResponse GetTemperature();
    IResponse GetHumidity();

    IResponse GetGratingId();
    IResponse SetGratingId(string gratingId);
    IResponse GetGratingAngle();
    IResponse SetGratingAngle(float gratingAngle);
    IResponse GetGratingWaveLength();
    IResponse SetGratingWaveLength(float gratingWaveLength);
    IResponse GetGratingDensity();
    IResponse SetGratingDensity(float gratingDensity);
    
    IResponse GetSlitId();
    IResponse SetSlitId(string slitId);
    IResponse GetSlitWidth();
    IResponse SetSlitWidth(float slitWidth);
    IResponse GetSlitAngle();
    IResponse SetSlitAngle(float slitAngle);
    
    IResponse GetFocusPosition();
    IResponse SetFocusPosition(float focusPosition);

    IResponse GetLightSource();
    IResponse SetLightSource(string lightSource);
}