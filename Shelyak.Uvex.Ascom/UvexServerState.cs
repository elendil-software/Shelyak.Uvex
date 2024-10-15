using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ASCOM.Utilities;
using Shelyak.Uvex.Shared;

namespace ASCOM.ShelyakUvex
{
    public class UvexServerState
    {
        private TraceLogger _logger;

        public UvexServerState(TraceLogger logger)
        {
            _logger = logger;
        }

        public bool UvexServerStartedByDriver { get; set; }

        public void CheckAndStartUvexServer()
        {
            _logger.LogMessage(nameof(UvexServerState), "Checking if Uvex server is running");
            if (IsUvexServerRunning())
            {
                _logger.LogMessage(nameof(UvexServerState), "Uvex server is already running");
            }
            else 
            {
                _logger.LogMessage(nameof(UvexServerState), "Uvex server is not running, starting it");
                StartUvexServer();
                UvexServerStartedByDriver = true;
                _logger.LogMessage(nameof(UvexServerState), "Uvex server started");
            }
        }
        
        public void CheckAndStopUvexServer()
        {
            if (UvexServerStartedByDriver)
            {
                StopUvexServer();
                UvexServerStartedByDriver = false;
            }
        }
        
        private void StopUvexServer()
        {
            try
            {
                var uvexServerProcess = Process.GetProcesses().FirstOrDefault(p => p.ProcessName.Contains(UvexConst.UvexProcess));
                uvexServerProcess?.Kill();
            }
            catch (Exception ex)
            {
                _logger.LogMessage(nameof(UvexServerState), $"An error occurred: {ex.Message}");
            }
        }

        private bool IsUvexServerRunning()
        {
            return Process.GetProcesses().Any(p => p.ProcessName.Contains(UvexConst.UvexProcess));
        }

        private void StartUvexServer()
        {
            try
            {
                _logger.LogMessage(nameof(UvexServerState), "Starting Uvex server");
                var shelyakUvexPath = Path.Combine(UvexConst.UvexInstallPath, UvexConst.UvexProcess + ".exe");
                _logger.LogMessage(nameof(UvexServerState), "Uvex server path: " + shelyakUvexPath);
                Process.Start(shelyakUvexPath, UvexConst.StartFromAscomArgument);
            }
            catch (Exception ex)
            {
                _logger.LogMessage(nameof(UvexServerState), $"An error occurred: {ex.Message}");
            }
        }
    }
}