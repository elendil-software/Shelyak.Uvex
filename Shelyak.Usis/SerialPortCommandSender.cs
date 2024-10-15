using System.IO.Ports;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shelyak.Usis.Commands;

namespace Shelyak.Usis
{
    public class SerialPortCommandSender : ICommandSender
    {
        private SerialPortSettings _settings;
        private readonly ILogger<SerialPortCommandSender> _logger;
        private readonly object _lock = new object();

        public SerialPortCommandSender(IOptionsMonitor<SerialPortSettings> settings, ILogger<SerialPortCommandSender> logger)
        {
            _settings = settings.CurrentValue;
            settings.OnChange(option =>
            {
                _settings = option;
            });
            _logger = logger;
        }

        public string SendCommand(ICommand command)
        {
            lock (_lock)
            {
                using (var serialPort = new SerialPort(_settings.PortName, _settings.BaudRate, _settings.Parity, _settings.DataBits, _settings.StopBits))
                {
                    serialPort.Handshake = _settings.Handshake;
                    serialPort.RtsEnable = _settings.RtsEnabled;
                    serialPort.DtrEnable = _settings.DtrEnabled;
                    serialPort.Encoding = Encoding.ASCII;
                    serialPort.ReadTimeout = _settings.ReadTimeout;
                    serialPort.WriteTimeout = _settings.WriteTimeout;
                    serialPort.Open();

                    string commandString = command.Build();
                    _logger.LogDebug("Sending command: {Command}", commandString);
                    serialPort.WriteLine(commandString);
                    string response = serialPort.ReadLine();
                    _logger.LogDebug("Received response: {Response}", response);

                    return response;
                }
            }
        }
    }
}