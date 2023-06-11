using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Responses;

public class ErrorResponse : IResponse
{
    public MessageErrorCode MessageErrorCode { get; set; }
    public string Message { get; set; }
}