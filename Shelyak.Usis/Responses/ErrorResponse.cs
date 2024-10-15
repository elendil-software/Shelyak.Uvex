using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Responses
{
    public class ErrorResponse<T> : IResponse
    {
        public MessageErrorCode MessageErrorCode { get; set; }
        public string Message { get; set; }
    }
}