using Shelyak.Usis;

namespace Shelyak.Uvex.Web.Endpoints.Config;

public interface ISettingsUpdater
{
    Task UpdateSerialPort(SerialPortSettings serialPortSettings);
    Task UpdateSwagger(bool enabled);
    Task UpdateGratingAngleStepSize(float stepSize);
    Task UpdateGratingWavelengthStepSize(float stepSize);
    Task UpdateFocusStepSize(float stepSize);
}