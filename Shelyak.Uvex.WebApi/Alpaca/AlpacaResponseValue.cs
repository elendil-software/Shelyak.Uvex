using Shelyak.Usis.Enums;

namespace Shelyak.Uvex.WebApi.Alpaca;

public class AlpacaResponseValue<T>
{
    public PropertyAttributeStatus Status { get; set; }
    public T Value { get; set; } = default!;
}