using System.Globalization;
using System.Text;
using Shelyak.Usis.Enums;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.Web.HttpClients
{
    public class UvexHttpClient : IUvexHttpClient
    {
        private readonly HttpClient _httpClient;

        public UvexHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AlpacaResponse<string>> GetDeviceName()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<string>>(ApiRoutes.DeviceName);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<string>> GetSoftwareVersion()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<string>>(ApiRoutes.SoftwareVersion);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<string>> GetProtocolVersion()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<string>>(ApiRoutes.ProtocolVersion);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetTemperature()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.Temperature);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetHumidity()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.Humidity);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<string>> GetGratingId()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<string>>(ApiRoutes.GratingId);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<string>> SetGratingId(string gratingId)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(ApiRoutes.GratingId, new StringContent(gratingId, Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AlpacaResponse<string>>();
        }

        public async Task<AlpacaResponse<float>> GetGratingAngle()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingAngle);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> SetGratingAngle(float newPosition)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(ApiRoutes.GratingAngle, new StringContent(newPosition.ToString(CultureInfo.InvariantCulture), Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AlpacaResponse<float>>();
        }

        public async Task<AlpacaResponse<float>> StopGratingAngle()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.StopGratingAngle);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> CalibrateGratingAngle(float gratingAngle)
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.CalibrateGratingAngle);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetGratingAngleMax()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingAngleMax);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetGratingAngleMin()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingAngleMin);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetGratingAnglePrec()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingAnglePrec);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetGratingWaveLength()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingWaveLength);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> SetGratingWaveLength(float gratingWaveLength)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(ApiRoutes.GratingWaveLength, new StringContent(gratingWaveLength.ToString(CultureInfo.InvariantCulture), Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AlpacaResponse<float>>();
        }

        public async Task<AlpacaResponse<float>> StopGratingWaveLength()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.StopGratingWaveLength);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> CalibrateGratingWaveLength(float gratingWaveLength)
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.CalibrateGratingWaveLength);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetGratingWaveLengthMax()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingWaveLengthMax);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetGratingWaveLengthMin()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingWaveLengthMin);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetGratingWaveLengthPrec()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingWaveLengthPrec);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetGratingDensity()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.GratingDensity);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> SetGratingDensity(float gratingDensity)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(ApiRoutes.GratingDensity, new StringContent(gratingDensity.ToString(CultureInfo.InvariantCulture), Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AlpacaResponse<float>>();
        }

        public async Task<AlpacaResponse<string>> GetSlitId()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<string>>(ApiRoutes.SlitId);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<string>> SetSlitId(string slitId)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(ApiRoutes.SlitId, new StringContent(slitId, Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AlpacaResponse<string>>();
        }

        public async Task<AlpacaResponse<float>> GetSlitWidth()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.SlitWidth);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> SetSlitWidth(float slitWidth)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(ApiRoutes.SlitWidth, new StringContent(slitWidth.ToString(CultureInfo.InvariantCulture), Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AlpacaResponse<float>>();
        }

        public async Task<AlpacaResponse<float>> GetSlitAngle()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.SlitAngle);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> SetSlitAngle(float slitAngle)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(ApiRoutes.SlitAngle, new StringContent(slitAngle.ToString(CultureInfo.InvariantCulture), Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AlpacaResponse<float>>();
        }

        public async Task<AlpacaResponse<float>> GetFocusPosition()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.FocusPosition);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> SetFocusPosition(float focusPosition)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(ApiRoutes.FocusPosition, new StringContent(focusPosition.ToString(CultureInfo.InvariantCulture), Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AlpacaResponse<float>>();
        }

        public async Task<AlpacaResponse<float>> StopFocusPosition()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.StopFocusPosition);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> CalibrateFocusPosition(float focusPosition)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(ApiRoutes.CalibrateFocusPosition, new StringContent(focusPosition.ToString(CultureInfo.InvariantCulture), Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AlpacaResponse<float>>();
        }

        public async Task<AlpacaResponse<float>> GetFocusPositionMax()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.FocusPositionMax);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetFocusPositionMin()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.FocusPositionMin);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<float>> GetFocusPositionPrec()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<float>>(ApiRoutes.FocusPositionPrec);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<LightSource>> GetLightSource()
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<LightSource>>(ApiRoutes.LightSource);
            return ReturnResponseIfSuccess(response);
        }

        public async Task<AlpacaResponse<LightSource>> SetLightSource(LightSource lightSource)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(ApiRoutes.LightSource, new StringContent(lightSource.ToString(), Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AlpacaResponse<LightSource>>();
        }
        
        private static AlpacaResponse<T> ReturnResponseIfSuccess<T>(AlpacaResponse<T> response)
        {
            response.EnsureSuccess();
            return response;
        }
    }
}