using System;
using System.Globalization;
using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Commands
{
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
            return $"{CommandType.SET};{_deviceProperty};{_attributeType};{SerializeValue(_value)}";
        }
        
        private string SerializeValue(T value)
        {
            switch (value)
            {
                case float floatValue:
                    return floatValue.ToString(CultureInfo.InvariantCulture);
                case int intValue:
                    return intValue.ToString();
                case string stringValue:
                    return stringValue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, "This type is not supported");
            }
        }   
    }
}