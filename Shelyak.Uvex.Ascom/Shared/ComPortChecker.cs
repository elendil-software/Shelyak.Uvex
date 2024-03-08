using System;
using System.Linq;

namespace ASCOM.ShelyakUvex.Shared
{
    public sealed class ComPortChecker : IDisposable
    {
        readonly ConfigHttpClient _httpClient;
        
        public ComPortChecker(string uvexApiUrl, int uvexApiPort)
        {
            _httpClient = UvexHttpClientHelper.CreateConfigHttpClient(
                UvexHttpClientHelper.BuildUvexUrl(uvexApiUrl, uvexApiPort, UvexApiParameter.defaultApiConfigPath));;
        }
        
        public bool CheckConnection()
        {
            var ports = _httpClient.GetAvailablePorts();
            var configuredPort = _httpClient.GetConfiguredPort();
            return ports.Contains(configuredPort);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}