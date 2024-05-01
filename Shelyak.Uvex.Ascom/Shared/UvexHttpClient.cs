using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Shelyak.Usis.Enums;
using Shelyak.Uvex.Alpaca;

namespace ASCOM.ShelyakUvex.Shared
{
    public sealed class UvexHttpClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        public UvexHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public AlpacaResponse<float> GetTemperature()
        {
            var response = _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.Temperature).GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(response);
        }
        
        public AlpacaResponse<float> GetFocusPosition()
        {
            var response = _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.FocusPosition).GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(response);
        }
        
        public AlpacaResponse<float> GetFocusPositionMax()
        {
            var response = _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.FocusPositionMax).GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(response);
        }
        
        public AlpacaResponse<float> GetFocusPositionPrecision()
        {
            var response = _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.FocusPositionPrec).GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(response);
        }

        
        
        public AlpacaResponse<float> MoveFocus(float position)
        {
            HttpResponseMessage response = _httpClient.PutAsync(ApiRoutes.FocusPosition, 
                BuildStringContent(position.ToString(CultureInfo.InvariantCulture))).GetAwaiter().GetResult();
            
            response.EnsureSuccessStatusCode();
            AlpacaResponse<float> alpacaResponse = response.Content.ReadFromJsonAsync<AlpacaResponse<float>>().GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(alpacaResponse);
        }

        public AlpacaResponse<float> StopFocus()
        {
            HttpResponseMessage response = _httpClient.PutAsync(ApiRoutes.StopFocusPosition, new StringContent("", Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var alpacaResponse = response.Content.ReadFromJsonAsync<AlpacaResponse<float>>().GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(alpacaResponse);
        }

        public AlpacaResponse<float> StopGratingAngle()
        {
            HttpResponseMessage response = _httpClient.PutAsync(ApiRoutes.StopGratingAngle, new StringContent("", Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var alpacaResponse = response.Content.ReadFromJsonAsync<AlpacaResponse<float>>().GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(alpacaResponse);
        }

        public AlpacaResponse<float> GetGratingAngle()
        {
            var response = _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingAngle).GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(response);
        }

        public AlpacaResponse<float> SetGratingAngle(float newPosition)
        {
            HttpResponseMessage response = _httpClient.PutAsync(ApiRoutes.GratingAngle, 
                BuildStringContent(newPosition.ToString(CultureInfo.InvariantCulture))).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var alpacaResponse = response.Content.ReadFromJsonAsync<AlpacaResponse<float>>().GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(alpacaResponse);
        }

        public AlpacaResponse<float> GetGratingAnglePrecision()
        {
            var response = _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingAnglePrec).GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(response);
        }

        public AlpacaResponse<float> CalibrateGratingAngle(float position)
        {
            HttpResponseMessage response = _httpClient.PutAsync(ApiRoutes.CalibrateGratingAngle, 
                BuildStringContent(position.ToString(CultureInfo.InvariantCulture))).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var alpacaResponse = response.Content.ReadFromJsonAsync<AlpacaResponse<float>>().GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(alpacaResponse);
        }

        public AlpacaResponse<LightSource> GetLightSource()
        {
            var response = _httpClient.GetFromJsonAsync<AlpacaResponse<LightSource>>(ApiRoutes.LightSource).GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(response);
        }

        public AlpacaResponse<LightSource> SetLightSource(LightSource lightSource)
        {
            HttpResponseMessage response = _httpClient.PutAsync(ApiRoutes.LightSource, BuildStringContent(((int)lightSource).ToString())).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var alpacaResponse = response.Content.ReadFromJsonAsync<AlpacaResponse<LightSource>>().GetAwaiter().GetResult();
            return ReturnResponseIfSuccess(alpacaResponse);
        }
        
        private static AlpacaResponse<T> ReturnResponseIfSuccess<T>(AlpacaResponse<T> response)
        {
            response.EnsureSuccess();
            return response;
        }
        
        private StringContent BuildStringContent(string value)
        {
            return new StringContent($"{{\"value\": {value}}}", Encoding.UTF8, "application/json");
        }


        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}