using Shelyak.Usis.Enums;
using Shelyak.Usis.Responses;

namespace Shelyak.Isis.Test.Responses
{
    public class ResponseParserShould
    {
        [Theory]
        [InlineData("C01;TIMEOUT*22", CommunicationErrorCode.C01_TIMEOUT, "TIMEOUT")]
        [InlineData("C01;TIMEOUT", CommunicationErrorCode.C01_TIMEOUT, "TIMEOUT")]
        [InlineData("C02;BAD REQUEST", CommunicationErrorCode.C02_BAD_REQUEST, "BAD REQUEST")]
        [InlineData("C03;BAD CHECKSUM", CommunicationErrorCode.C03_BAD_CHECKSUM, "BAD CHECKSUM")]
        [InlineData("C04;OVERFLOW", CommunicationErrorCode.C04_OVERFLOW, "OVERFLOW")]
        public void ParseCommunicationErrorResponse(string errorString, CommunicationErrorCode errorCode, string message)
        {
            IResponse response = ResponseParser.Parse<int>(errorString);
            Assert.IsType<CommunicationErrorResponse>(response);
            Assert.Equal(errorCode, ((CommunicationErrorResponse)response).ErrorCode);
            Assert.Equal(message, ((CommunicationErrorResponse)response).Message);
        }
        
        [Theory]
        [InlineData("M00;GRATING_ANGLE;VALUE;OK;45.27", MessageErrorCode.M00_OK, DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE, PropertyAttributeStatus.OK, 45.27)]
        public void ParseSuccessResponse(string responseString, 
            MessageErrorCode messageErrorCode, 
            DeviceProperty deviceProperty, 
            PropertyAttributeType propertyAttributeType, 
            PropertyAttributeStatus propertyAttributeStatus, 
            double value)
        {
            IResponse response = ResponseParser.Parse<double>(responseString);
                
            Assert.IsType<SuccessResponse<double>>(response);
            Assert.Equal(messageErrorCode, ((SuccessResponse<double>)response).MessageErrorCode);
            Assert.Equal(deviceProperty, ((SuccessResponse<double>)response).DeviceProperty);
            Assert.Equal(propertyAttributeType, ((SuccessResponse<double>)response).PropertyAttributeType);
            Assert.Equal(propertyAttributeStatus, ((SuccessResponse<double>)response).PropertyAttributeStatus);
            Assert.Equal(value, ((SuccessResponse<double>)response).Value);
        }
    }
}