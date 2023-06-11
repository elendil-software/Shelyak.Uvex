namespace Shelyak.Uvex.WebApi.Alpaca;

public enum AlpacaError
{
    NoError = 0,
    PropertyOrMethodNotImplemented = 0x400, //(1024)
    InvalidValue = 0x401, //(1025)
    ValueNotSet = 0x402, //(1026)
    NotConnected = 0x407, //(1031)
    InvalidWhileParked = 0x408, //(1032)
    InvalidWhileSlaved = 0x409, //(1033)
    InvalidOperation = 0x40B, //(1035)
    ActionNotImplemented = 0x40C, //(1036)
}