namespace Shelyak.Uvex.Alpaca
{

    public class AlpacaResponseValue<T>
    {
        public int Status { get; set; }
        public T Value { get; set; } = default;
    }
}