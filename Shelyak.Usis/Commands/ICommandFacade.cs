using Shelyak.Usis.Enums;
using Shelyak.Usis.Responses;

namespace Shelyak.Usis.Commands
{
    public interface ICommandFacade
    {
        IResponse ExecuteCommand<T>(CommandType commandType, DeviceProperty gratingAngle, PropertyAttributeType propertyAttributeType, T value);
    }
}