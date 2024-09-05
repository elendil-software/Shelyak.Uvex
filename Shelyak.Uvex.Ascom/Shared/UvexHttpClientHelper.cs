using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using Shelyak.Uvex.Shared;

namespace ASCOM.ShelyakUvex.Shared
{
    internal static class UvexHttpClientHelper
    {
        public static string UvexUrl = LoadUvexUrl();
        
        private static string LoadUvexUrl()
        {
            string uvexSettingsFilePath = UvexSettingsFilePathProvider.CreateProductionInstance.UvexSettingsFilePath;
            string jsonString = File.ReadAllText(uvexSettingsFilePath);

            string url = null;
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
                if (root.TryGetProperty("Kestrel", out JsonElement kestrelElement) &&
                    kestrelElement.TryGetProperty("EndPoints", out JsonElement endPointsElement) &&
                    endPointsElement.TryGetProperty("Http", out JsonElement httpElement) &&
                    httpElement.TryGetProperty("Url", out JsonElement urlElement))
                {
                    url = urlElement.GetString();
                }
            }

            return ApplicationUrlHelper.ParseApplicationUrl(url);
        }
        
        public static UvexHttpClient CreateUvexHttpClient(string uvexUrl)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(uvexUrl);
            return new UvexHttpClient(httpClient);
        }
        
        public static ConfigHttpClient CreateConfigHttpClient(string uvexUrl)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(uvexUrl);
            return new ConfigHttpClient(httpClient);
        }

        public static string BuildUvexUrl(string path)
        {
            return $"{UvexUrl}/{path.Trim('/')}/";
        }
    }
}