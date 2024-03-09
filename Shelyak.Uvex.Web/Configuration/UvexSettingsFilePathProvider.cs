namespace Shelyak.Uvex.Web.Configuration;

public static class UvexSettingsFilePathProvider
{
    public static string UvexSettingsFilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Shelyak/Uvex/appsettings-uvex.json");
}