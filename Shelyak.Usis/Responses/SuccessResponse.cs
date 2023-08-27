using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Responses
{
    public class SuccessResponse<T> : IResponse<T>
    {
        public MessageErrorCode MessageErrorCode { get; set; }
        public DeviceProperty DeviceProperty { get; set; }
        public PropertyAttributeType PropertyAttributeType { get; set; }
        public PropertyAttributeStatus PropertyAttributeStatus { get; set; }
        public T Value { get; set; }
    }
}