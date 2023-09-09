using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using ASCOM.Utilities.Interfaces;
using Shelyak.Uvex.Alpaca;


namespace ASCOM.ShelyakUvex.Focuser
{
    public class UvexHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ITraceLoggerExtra _traceLogger;

        public UvexHttpClient(HttpClient httpClient, ITraceLoggerExtra traceLogger)
        {
            _httpClient = httpClient;
            _traceLogger = traceLogger;
        }
        
        public AlpacaResponse<float> GetTemperature()
        {
            return _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.Temperature).GetAwaiter().GetResult();
        }

        public AlpacaResponse<float> GetFocusPosition()
        {
            return _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.FocusPosition).GetAwaiter().GetResult();
        }
        
        public AlpacaResponse<float> GetFocusPositionMax()
        {
            return _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.FocusPositionMax).GetAwaiter().GetResult();
        }
        
        public AlpacaResponse<float> GetFocusPositionPrecision()
        {
            return _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.FocusPositionPrec).GetAwaiter().GetResult();
        }
        
        public AlpacaResponse<string> MoveFocus(float position)
        {
            HttpResponseMessage response = _httpClient.PutAsync(ApiRoutes.FocusPosition, new StringContent(position.ToString())).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<AlpacaResponse<string>>().GetAwaiter().GetResult();
        }

        public AlpacaResponse<string> StopFocus()
        {
            HttpResponseMessage response = _httpClient.PutAsync(ApiRoutes.StopFocusPosition, new StringContent("")).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<AlpacaResponse<string>>().GetAwaiter().GetResult();
        }

        public AlpacaResponse<float> StopGratingAngle()
        {
            HttpResponseMessage response = _httpClient.PutAsync(ApiRoutes.GratingAngle, new StringContent("")).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<AlpacaResponse<float>>().GetAwaiter().GetResult();
        }

        public AlpacaResponse<float> GetGratingAngle()
        {
            return _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingAngle).GetAwaiter().GetResult();
        }

        public AlpacaResponse<float> SetGratingAngle(float newPosition)
        {
            HttpResponseMessage response = _httpClient.PutAsync(ApiRoutes.GratingAngle, new StringContent(newPosition.ToString())).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<AlpacaResponse<float>>().GetAwaiter().GetResult();
        }

        public AlpacaResponse<float> GetGratingAnglePrecision()
        {
            return _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingAnglePrec).GetAwaiter().GetResult();
        }

        public AlpacaResponse<float> CalibrateGratingAngle(float position)
        {
            HttpResponseMessage response = _httpClient.PutAsync(ApiRoutes.CalibrateGratingAngle, new StringContent(position.ToString(CultureInfo.InvariantCulture))).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<AlpacaResponse<float>>().GetAwaiter().GetResult();
        }
    }
}