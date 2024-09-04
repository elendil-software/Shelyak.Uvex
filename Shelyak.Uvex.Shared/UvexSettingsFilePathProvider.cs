using System;
using System.IO;

namespace Shelyak.Uvex.Shared
{
    public class UvexSettingsFilePathProvider : IUvexSettingsFilePathProvider
    {
        public string UvexSettingsFilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Shelyak/Uvex/appsettings-uvex.json");

        public static IUvexSettingsFilePathProvider CreateProductionInstance => new UvexSettingsFilePathProvider();
    }
}