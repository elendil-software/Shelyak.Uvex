using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Shelyak.Uvex.Alpaca;

namespace ASCOM.ShelyakUvex.Shared
{
    public sealed class ConfigHttpClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        
        public ConfigHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public string GetConfiguredPort()
        {
            return _httpClient.GetStringAsync(ApiRoutes.ConfigPort).GetAwaiter().GetResult();
        }
        
        public void UpdatePort(string portName)
        {
            _httpClient.PutAsync(ApiRoutes.ConfigPort, new StringContent($"\"{portName}\"", Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
        }
        
        public string[] GetAvailablePorts()
        {
            return _httpClient.GetFromJsonAsync<string[]>(ApiRoutes.ConfigPorts).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}