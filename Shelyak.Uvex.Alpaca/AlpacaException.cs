using System;

namespace Shelyak.Uvex.Alpaca
{
    public class AlpacaException : Exception
    {
        public AlpacaError ErrorNumber { get; }

        public AlpacaException(AlpacaError errorNumber, string message) : base(message)
        {
            ErrorNumber = errorNumber;
        }
    }
}