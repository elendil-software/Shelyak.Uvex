using System;
using System.Linq;
using ASCOM.Utilities;

namespace ASCOM.ShelyakUvex.Shared
{
    public sealed class ComPortChecker : IDisposable
    {
        private readonly TraceLogger _tl;
        private readonly ConfigHttpClient _httpClient;
        
        public ComPortChecker(TraceLogger tl)
        {
            _tl = tl;
            _httpClient = UvexHttpClientHelper.CreateConfigHttpClient(UvexHttpClientHelper.BuildUvexUrl(UvexApiParameter.defaultApiConfigPath));
        }
        
        public bool CheckConnection()
        {
            try
            {
                var ports = _httpClient.GetAvailablePorts();
                var configuredPort = _httpClient.GetConfiguredPort();
                return ports.Contains(configuredPort);
            }
            catch (Exception e)
            {
                _tl.LogMessage(nameof(CheckConnection), "Unable to connect to the device. Check that the UVEX is connected to the PC and the UVEX service is started");
                throw new NotConnectedException("Unable to connect to the device. Check that the UVEX is connected to the PC and the UVEX service is started", e);
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}