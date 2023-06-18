using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Commands;

public class StopCommand<T> : ICommand
{
    private readonly DeviceProperty _deviceProperty;

    public StopCommand(DeviceProperty deviceProperty)
    {
        _deviceProperty = deviceProperty;
    }

    public string Build()
    {
        return $"{CommandType.STOP};{_deviceProperty}";
    }
}