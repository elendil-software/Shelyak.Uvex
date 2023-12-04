using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.WebApi.Controllers;

public static class AlpacaResponseBuilder
{
    public static AlpacaResponse<T> BuildAlpacaResponse<T>(uint clientTransactionId, uint serverTransactionId, Exception e)
    {
        return new AlpacaResponse<T>
        {
            ClientTransactionID = clientTransactionId,
            ServerTransactionID = serverTransactionId,
            ErrorNumber = AlpacaError.InvalidOperation,
            ErrorMessage = e.Message
        };
    }

    public static AlpacaResponse<T> BuildAlpacaResponse<T>(uint clientTransactionId, uint serverTransactionId, IResponse<T> response)
    {
        var alpacaResponse = new AlpacaResponse<T>
        {
            ClientTransactionID = clientTransactionId,
            ServerTransactionID = serverTransactionId
        };

        if (response is SuccessResponse<T> successResponse)
        {
            alpacaResponse.Value = new AlpacaResponseValue<T>
            {
                Status = (int) successResponse.PropertyAttributeStatus,
                Value = successResponse.Value
            };
            alpacaResponse.ErrorNumber = CodeConverter.ConvertMessageCode(successResponse.MessageErrorCode);
            alpacaResponse.ErrorMessage = successResponse.MessageErrorCode.ToString();
        }
        else if(response is ErrorResponse<T> errorResponse)
        {
            alpacaResponse.ErrorNumber = CodeConverter.ConvertMessageCode(errorResponse.MessageErrorCode);
            alpacaResponse.ErrorMessage = errorResponse.Message;
        }
        else if (response is CommunicationErrorResponse<T> communicationErrorResponse)
        {
            alpacaResponse.ErrorNumber = CodeConverter.ConvertErrorCode(communicationErrorResponse.ErrorCode);
            alpacaResponse.ErrorMessage = communicationErrorResponse.Message;
        }

        return alpacaResponse;
    }
}