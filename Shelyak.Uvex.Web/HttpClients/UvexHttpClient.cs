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

        #region Device
        
        public async Task<AlpacaResponse<string>> GetDeviceName() 
            => await GetAsync<string>(ApiRoutes.DeviceName);
        public async Task<AlpacaResponse<string>> GetSoftwareVersion() 
            => await GetAsync<string>(ApiRoutes.SoftwareVersion);
        public async Task<AlpacaResponse<string>> GetProtocolVersion() 
            => await GetAsync<string>(ApiRoutes.ProtocolVersion);
        public async Task<AlpacaResponse<float>> GetTemperature() 
            => await GetAsync<float>(ApiRoutes.Temperature);
        public async Task<AlpacaResponse<float>> GetHumidity() 
            => await GetAsync<float>(ApiRoutes.Humidity);

        #endregion
        
        #region Grating
        
        public async Task<AlpacaResponse<string>> GetGratingId() 
            => await GetAsync<string>(ApiRoutes.GratingId);
        
        public async Task<AlpacaResponse<string>> SetGratingId(string gratingId) 
            => await PutAsync<string>(ApiRoutes.GratingId, gratingId);
        
        public async Task<AlpacaResponse<float>> GetGratingAngle() 
            => await GetAsync<float>(ApiRoutes.GratingAngle);

        public async Task<AlpacaResponse<float>> SetGratingAngle(float newPosition) 
            => await PutAsync<float>(ApiRoutes.GratingAngle, newPosition.ToString(CultureInfo.InvariantCulture));
        
        public async Task<AlpacaResponse<float>> StopGratingAngle() 
            => await PutEmptyBodyAsync<float>(ApiRoutes.StopGratingAngle);
        
        public async Task<AlpacaResponse<float>> CalibrateGratingAngle(float gratingAngle) 
            => await PutAsync<float>(ApiRoutes.CalibrateGratingAngle, gratingAngle.ToString(CultureInfo.InvariantCulture));
        
        public async Task<AlpacaResponse<float>> GetGratingAngleMax() 
            => await GetAsync<float>(ApiRoutes.GratingAngleMax);

        public async Task<AlpacaResponse<float>> GetGratingAngleMin() 
            => await GetAsync<float>(ApiRoutes.GratingAngleMin);

        public async Task<AlpacaResponse<float>> GetGratingAnglePrec() 
            => await GetAsync<float>(ApiRoutes.GratingAnglePrec);

        public async Task<AlpacaResponse<float>> GetGratingWaveLength() 
            => await GetAsync<float>(ApiRoutes.GratingWaveLength);

        public async Task<AlpacaResponse<float>> SetGratingWaveLength(float gratingWaveLength)
            => await PutAsync<float>(ApiRoutes.GratingWaveLength, gratingWaveLength.ToString(CultureInfo.InvariantCulture));

        public async Task<AlpacaResponse<float>> StopGratingWaveLength() 
            => await PutEmptyBodyAsync<float>(ApiRoutes.StopGratingWaveLength);
        
        public async Task<AlpacaResponse<float>> CalibrateGratingWaveLength(float gratingWaveLength) 
            => await PutAsync<float>(ApiRoutes.CalibrateGratingWaveLength, gratingWaveLength.ToString(CultureInfo.InvariantCulture));

        public async Task<AlpacaResponse<float>> GetGratingWaveLengthMax()
            => await GetAsync<float>(ApiRoutes.GratingWaveLengthMax);

        public async Task<AlpacaResponse<float>> GetGratingWaveLengthMin()
            => await GetAsync<float>(ApiRoutes.GratingWaveLengthMin);

        public async Task<AlpacaResponse<float>> GetGratingWaveLengthPrec()
            => await GetAsync<float>(ApiRoutes.GratingWaveLengthPrec);

        public async Task<AlpacaResponse<float>> GetGratingDensity()
            => await GetAsync<float>(ApiRoutes.GratingDensity);

        public async Task<AlpacaResponse<float>> SetGratingDensity(float gratingDensity)
            => await PutAsync<float>(ApiRoutes.GratingDensity, gratingDensity.ToString(CultureInfo.InvariantCulture));

        #endregion

        #region Slit
        
        public async Task<AlpacaResponse<string>> GetSlitId()
            => await GetAsync<string>(ApiRoutes.SlitId);

        public async Task<AlpacaResponse<string>> SetSlitId(string slitId)
            => await PutAsync<string>(ApiRoutes.SlitId, slitId);

        public async Task<AlpacaResponse<float>> GetSlitWidth()
            => await GetAsync<float>(ApiRoutes.SlitWidth);

        public async Task<AlpacaResponse<float>> SetSlitWidth(float slitWidth)
            => await PutAsync<float>(ApiRoutes.SlitWidth, slitWidth.ToString(CultureInfo.InvariantCulture));

        public async Task<AlpacaResponse<float>> GetSlitAngle()
            => await GetAsync<float>(ApiRoutes.SlitAngle);

        public async Task<AlpacaResponse<float>> SetSlitAngle(float slitAngle)
            => await PutAsync<float>(ApiRoutes.SlitAngle, slitAngle.ToString(CultureInfo.InvariantCulture));
        
        #endregion
        
        #region Focus

        public async Task<AlpacaResponse<float>> GetFocusPosition()
            => await GetAsync<float>(ApiRoutes.FocusPosition);

        public async Task<AlpacaResponse<float>> SetFocusPosition(float focusPosition)
            => await PutAsync<float>(ApiRoutes.FocusPosition, focusPosition.ToString(CultureInfo.InvariantCulture));

        public async Task<AlpacaResponse<float>> StopFocusPosition()
            => await PutEmptyBodyAsync<float>(ApiRoutes.StopFocusPosition);
        
        public async Task<AlpacaResponse<float>> CalibrateFocusPosition(float focusPosition) => 
            await PutAsync<float>(ApiRoutes.CalibrateFocusPosition, focusPosition.ToString(CultureInfo.InvariantCulture));
        
        public async Task<AlpacaResponse<float>> GetFocusPositionMax()
        => await GetAsync<float>(ApiRoutes.FocusPositionMax);

        public async Task<AlpacaResponse<float>> GetFocusPositionMin() 
            => await GetAsync<float>(ApiRoutes.FocusPositionMin);

        public async Task<AlpacaResponse<float>> GetFocusPositionPrec()
        => await GetAsync<float>(ApiRoutes.FocusPositionPrec);

        public async Task<AlpacaResponse<LightSource>> GetLightSource()
            => await GetAsync<LightSource>(ApiRoutes.LightSource);

        public async Task<AlpacaResponse<LightSource>> SetLightSource(LightSource lightSource)
            => await PutAsync<LightSource>(ApiRoutes.LightSource, ((int)lightSource).ToString());
        
        #endregion
        
        private async Task<AlpacaResponse<T>> GetAsync<T>(string route)
        {
            var response = await _httpClient.GetFromJsonAsync<AlpacaResponse<T>>(route);
            return ReturnResponseIfSuccess(response);
        }
        
        private async Task<AlpacaResponse<T>> PutAsync<T>(string route, string body)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(route, new StringContent(body, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<AlpacaResponse<T>>();
            return ReturnResponseIfSuccess(result);
        }
        
        private Task<AlpacaResponse<T>> PutEmptyBodyAsync<T>(string route) => PutAsync<T>(route, string.Empty);
        
        private static AlpacaResponse<T> ReturnResponseIfSuccess<T>(AlpacaResponse<T> response)
        {
            response.EnsureSuccess();
            return response;
        }
    }
}