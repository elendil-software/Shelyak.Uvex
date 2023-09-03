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
    }
}