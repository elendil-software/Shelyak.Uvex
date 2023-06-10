namespace Shelyak.Uvex.WebApi.Alpaca;

public class AlpacaResponse<T>
{
    /// <summary>
    ///  Transaction ID supplied by the client in its request
    /// </summary>
    public uint ClientTransactionID { get; set; }
    
    /// <summary>
    /// The server’s transaction number
    /// </summary>
    public uint ServerTransactionID { get; set; }
    
    /// <summary>
    /// ASCOM Alpaca error number, see section 2.8.3.
    /// </summary>
    public AlpacaError ErrorNumber { get; set; } = AlpacaError.NoError;

    /// <summary>
    /// If the driver throws an exception, its message appears here, otherwise an empty string is returned
    /// </summary>
    public string ErrorMessage { get; set; } = "";

    public T Value { get; set; } = default!;
}