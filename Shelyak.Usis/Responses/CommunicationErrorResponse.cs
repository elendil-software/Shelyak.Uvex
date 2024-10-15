using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Responses
{
    public class CommunicationErrorResponse<T> : IResponse
    {
        public CommunicationErrorCode ErrorCode { get; set; }
        public string Message { get; set; }
    }
}