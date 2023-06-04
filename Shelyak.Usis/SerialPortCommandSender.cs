using System.IO.Ports;
using System.Text;
using Microsoft.Extensions.Options;
using Shelyak.Usis;
using Shelyak.Usis.Commands;

public class SerialPortCommandSender : ICommandSender
{
    private readonly SerialPortSettings _settings;
    private readonly object _lock = new();

    public SerialPortCommandSender(IOptions<SerialPortSettings> settings)
    {
        _settings = settings.Value;
    }

    public string SendCommand(ICommand command)
    {
        lock (_lock)
        {
            using var serialPort = new SerialPort(_settings.PortName, _settings.BaudRate, _settings.Parity, _settings.DataBits, _settings.StopBits);
            serialPort.Handshake = _settings.Handshake;
            serialPort.RtsEnable = _settings.RtsEnabled;
            serialPort.DtrEnable = _settings.DtrEnabled;
            serialPort.Encoding = Encoding.ASCII;
            serialPort.Open();
            serialPort.WriteLine(command.Build());

            return serialPort.ReadLine();
        }
    }
}