using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

public static class AlpacaResponseBuilder
{
    public static AlpacaResponse<T> BuildAlpacaResponse<T>(uint? clientTransactionId, uint serverTransactionId, Exception e)
    {
        return new AlpacaResponse<T>
        {
            ClientTransactionID = clientTransactionId ?? 0,
            ServerTransactionID = serverTransactionId,
            ErrorNumber = GetAlpacaError(e),
            ErrorMessage = e.Message
        };
    }
    
    private static AlpacaError GetAlpacaError(Exception e)
    {
        return e switch
        {
            FileNotFoundException => AlpacaError.NotConnected,
            InvalidOperationException => AlpacaError.InvalidOperation,
            _ => AlpacaError.InvalidOperation
        };
    }

    public static AlpacaResponse<T> BuildAlpacaResponse<T>(uint? clientTransactionId, uint serverTransactionId, IResponse response)
    {
        var alpacaResponse = new AlpacaResponse<T>
        {
            ClientTransactionID = clientTransactionId ?? 0,
            ServerTransactionID = serverTransactionId
        };

        if (response is SuccessResponse<T> successResponse)
        {
            alpacaResponse.Value = new AlpacaResponseValue<T>
            {
                Status = (int) successResponse.PropertyAttributeStatus,
                Value = successResponse.Value
            };
            alpacaResponse.ErrorNumber = AlpacaErrorCodeConverter.ConvertMessageCode(successResponse.MessageErrorCode);
            alpacaResponse.ErrorMessage = successResponse.MessageErrorCode.ToString();
        }
        else if(response is ErrorResponse errorResponse)
        {
            alpacaResponse.ErrorNumber = AlpacaErrorCodeConverter.ConvertMessageCode(errorResponse.MessageErrorCode);
            alpacaResponse.ErrorMessage = errorResponse.Message;
        }
        else if (response is CommunicationErrorResponse communicationErrorResponse)
        {
            alpacaResponse.ErrorNumber = AlpacaErrorCodeConverter.ConvertErrorCode(communicationErrorResponse.ErrorCode);
            alpacaResponse.ErrorMessage = communicationErrorResponse.Message;
        }

        return alpacaResponse;
    }
}