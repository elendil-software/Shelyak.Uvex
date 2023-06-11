using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Commands;

public class SetCommand<T> : ICommand
{
    private readonly DeviceProperty _deviceProperty;
    private readonly PropertyAttributeType _attributeType;
    private readonly T _value;

    public SetCommand(DeviceProperty deviceProperty, PropertyAttributeType attributeType, T value)
    {
        _deviceProperty = deviceProperty;
        _attributeType = attributeType;
        _value = value;
    }

    public string Build()
    {
        return $"{CommandType.SET};{_deviceProperty};{_attributeType};{_value}";
    }
}