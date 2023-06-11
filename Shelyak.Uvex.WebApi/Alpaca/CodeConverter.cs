using Shelyak.Usis.Enums;
using Shelyak.Uvex.WebApi.Alpaca;

namespace Shelyak.Uvex.WebApi.Controllers;

public static class CodeConverter
{
    public static AlpacaError ConvertMessageCode(MessageErrorCode successResponseMessageErrorCode)
    {
        return successResponseMessageErrorCode switch
        {
            MessageErrorCode.M00_OK => AlpacaError.NoError,
            MessageErrorCode.M01_UNKNOWN_PROPERTY => AlpacaError.InvalidOperation,
            MessageErrorCode.M02_UNKNOWN_ATTRIBUTE => AlpacaError.InvalidOperation,
            MessageErrorCode.M03_READONLY => AlpacaError.InvalidOperation,
            MessageErrorCode.M04_BAD_VALUE_TYPE => AlpacaError.InvalidValue,
            MessageErrorCode.M05_NO_VALUE_GIVEN => AlpacaError.InvalidValue,
            MessageErrorCode.M06_UNKNOWN_COMMAND => AlpacaError.InvalidOperation,
            MessageErrorCode.M07_OUT_OF_RANGE => AlpacaError.InvalidValue,
            MessageErrorCode.M08_BAD_VALUE => AlpacaError.InvalidValue,
            MessageErrorCode.M09_BAD_INDEX => AlpacaError.InvalidValue,
            MessageErrorCode.M10_NO_POWER => AlpacaError.InvalidOperation,
            _ => throw new ArgumentOutOfRangeException(nameof(successResponseMessageErrorCode), successResponseMessageErrorCode, null)
        };
    }

    public static AlpacaError ConvertErrorCode(CommunicationErrorCode errorResponseErrorCode)
    {
        return errorResponseErrorCode switch
        {
            CommunicationErrorCode.C01_TIMEOUT => AlpacaError.InvalidOperation,
            CommunicationErrorCode.C02_BAD_REQUEST => AlpacaError.InvalidOperation,
            CommunicationErrorCode.C03_BAD_CHECKSUM => AlpacaError.InvalidOperation,
            CommunicationErrorCode.C04_OVERFLOW => AlpacaError.InvalidValue,
            _ => throw new ArgumentOutOfRangeException(nameof(errorResponseErrorCode), errorResponseErrorCode, null)
        };
    }
}