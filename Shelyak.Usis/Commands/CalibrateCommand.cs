using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Commands;

public class CalibrateCommand<T> : ICommand
{
    private readonly DeviceProperty _deviceProperty;
    private readonly T _value;

    public CalibrateCommand(DeviceProperty deviceProperty, T value)
    {
        _deviceProperty = deviceProperty;
        _value = value;
    }

    public string Build()
    {
        return $"{CommandType.CALIB};{_deviceProperty};{_value}";
    }
}