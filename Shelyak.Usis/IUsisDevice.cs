using Shelyak.Usis.Enums;
using Shelyak.Usis.Responses;

namespace Shelyak.Usis
{
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
        IResponse StopGratingAngle();
        IResponse CalibrateGratingAngle(float gratingAngle);
        IResponse GetGratingAngleMax();
        IResponse GetGratingAngleMin();
        IResponse GetGratingAnglePrec();

        IResponse GetGratingWaveLength();
        IResponse SetGratingWaveLength(float gratingWaveLength);
        IResponse StopGratingWaveLength();
        IResponse CalibrateGratingWaveLength(float gratingWaveLength);
        IResponse GetGratingWaveLengthMax();
        IResponse GetGratingWaveLengthMin();
        IResponse GetGratingWaveLengthPrec();

        IResponse GetGratingDensity();
        IResponse SetGratingDensity(float gratingDensity);
    
    
        IResponse GetSlitId();
        IResponse SetSlitId(string slitId);

        IResponse GetSlitWidth();
        IResponse SetSlitWidth(float slitWidth);

        IResponse GetSlitAngle();
        IResponse SetSlitAngle(float slitAngle);
        IResponse StopSlitAngle();
        IResponse CalibrateSlitAngle(float slitAngle);

        IResponse GetFocusPosition();
        IResponse SetFocusPosition(float focusPosition);
        IResponse StopFocusPosition();
        IResponse CalibrateFocusPosition(float focusPosition);
        IResponse GetFocusPositionMax();
        IResponse GetFocusPositionMin();
        IResponse GetFocusPositionPrec();

        IResponse GetLightSource();
        IResponse SetLightSource(LightSource lightSource);
    }
}