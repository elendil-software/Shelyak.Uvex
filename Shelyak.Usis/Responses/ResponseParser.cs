using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Responses
{
    public class ResponseParser : IResponseParser
    {
        private readonly ILogger<ResponseParser> _logger;

        public ResponseParser(ILogger<ResponseParser> logger)
        {
            _logger = logger;
        }

        public IResponse Parse<T>(string responseString)
        {
            _logger.LogDebug("Parsing response: {Response}", responseString);
        
            if (responseString.StartsWith("M"))
            {
                _logger.LogDebug("Response is USIS response");
                return ParseUsisResponse<T>(responseString);
            }

            if (responseString.StartsWith("C"))
            {
                _logger.LogDebug("Response is communication error response");
                return ParseCommunicationErrorResponse<T>(responseString);
            }

            throw new ArgumentException("Unknown response type");
        }

        private IResponse ParseCommunicationErrorResponse<T>(string responseString)
        {
            string[] parts = responseString.TrimEnd('\n').Split(';');
            
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid response format");
            }
            
            var response = new CommunicationErrorResponse<T>
            {
                ErrorCode = (CommunicationErrorCode)int.Parse(parts[0].Substring(1)),
                Message = $"{parts[0]} - {parts[1].Split('*')[0].TrimEnd('\n')}"
            };
        
            _logger.LogDebug("Parsed response: {@Response}", response);

            return response;
        }
        
        private IResponse ParseUsisResponse<T>(string responseString)
        {
            string[] parts = responseString.TrimEnd('\n').Split(';');

            if (parts[0] == "M00")
            {
                _logger.LogDebug("Response is success response");
                var response = new SuccessResponse<T>
                {
                    MessageErrorCode = (MessageErrorCode)int.Parse(parts[0].Substring(1)),
                    DeviceProperty = (DeviceProperty)Enum.Parse(typeof(DeviceProperty), parts[1]),
                    PropertyAttributeType = (PropertyAttributeType)Enum.Parse(typeof(PropertyAttributeType), parts[2]),
                    PropertyAttributeStatus = (PropertyAttributeStatus)Enum.Parse(typeof(PropertyAttributeStatus), parts[3]),
                    Value = ConvertTo<T>(parts[4])
                };
            
                _logger.LogDebug("Parsed response: {@Response}", response);
            
                return response;
            }
            else
            {
                _logger.LogDebug("Response is error response");
                var response = new ErrorResponse<T>
                {
                    MessageErrorCode = (MessageErrorCode)int.Parse(parts[0].Substring(1)),
                    Message = $"{parts[0]} - {parts[1].Split('*')[0].TrimEnd('\n')}"
                };
            
                _logger.LogDebug("Parsed response: {@Response}", response);

                return response;
            }
        }
        
        private static T ConvertTo<T>(string value)
        {
            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), value);
            }

            Type nullableType = Nullable.GetUnderlyingType(typeof(T));
            if (nullableType != null)
            {
                return (T) Convert.ChangeType(value, nullableType, CultureInfo.InvariantCulture);
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}