using System.Globalization;

namespace Shelyak.Uvex.Web.Locales;

public static class Cultures
{
    public static CultureInfo[] SupportedCultures =>
    [
        new CultureInfo("en"),
        new CultureInfo("fr")
    ];
}