﻿using System.IO.Ports;

namespace Shelyak.Usis
{
    public class SerialPortSettings
    {
        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;
        public Parity Parity { get; set; } = Parity.None;
        public int DataBits { get; set; } = 8;
        public StopBits StopBits { get; set; } = StopBits.One;
        public Handshake Handshake { get; set; } = Handshake.None;
        public bool RtsEnabled { get; set; } = true;
        public bool DtrEnabled { get; set; } = true;
        public int ReadTimeout { get; set; } = SerialPort.InfiniteTimeout;
        public int WriteTimeout { get; set; } = SerialPort.InfiniteTimeout;
    }
}