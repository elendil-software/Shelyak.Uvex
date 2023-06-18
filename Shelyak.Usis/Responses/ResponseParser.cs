using System.Globalization;
using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Responses;

public static class ResponseParser
{
    public static IResponse Parse<T>(string responseString)
    {
        if (responseString.StartsWith('M'))
        {
            return ParseUsisResponse<T>(responseString);
        }

        if (responseString.StartsWith('C'))
        {
            return ParseCommunicationErrorResponse(responseString);
        }

        throw new ArgumentException("Unknown response type");
    }

    private static IResponse ParseCommunicationErrorResponse(string responseString)
    {
        string[] parts = responseString.TrimEnd('\n').Split(';');
            
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid response format");
        }
            
        var response = new CommunicationErrorResponse
        {
            ErrorCode = (CommunicationErrorCode)int.Parse(parts[0][1..]),
            Message = parts[1].Split('*')[0].TrimEnd('\n')
        };

        return response;
    }

    private static IResponse ParseUsisResponse<T>(string responseString)
    {
        string[] parts = responseString.TrimEnd('\n').Split(';');

        if (parts[0] == "M00")
        {

            var response = new SuccessResponse<T>
            {
                MessageErrorCode = (MessageErrorCode)int.Parse(parts[0][1..]),
                DeviceProperty = (DeviceProperty)Enum.Parse(typeof(DeviceProperty), parts[1]),
                PropertyAttributeType = (PropertyAttributeType)Enum.Parse(typeof(PropertyAttributeType), parts[2]),
                PropertyAttributeStatus = (PropertyAttributeStatus)Enum.Parse(typeof(PropertyAttributeStatus), parts[3]),
                Value = (T)Convert.ChangeType(parts[4], typeof(T), CultureInfo.InvariantCulture)
            };
            
            return response;
        }
        else
        {
            var response = new ErrorResponse
            {
                MessageErrorCode = (MessageErrorCode)int.Parse(parts[0][1..]), 
                Message = parts[1].Split('*')[0].TrimEnd('\n')
            };

            return response;
        }
    }
}