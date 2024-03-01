using System;
using System.Net.Http;
using ASCOM.ShelyakUvex.Focuser;

namespace ASCOM.ShelyakUvex.Shared
{
    internal static class UvexHttpClientHelper
    {
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

        public static string BuildUvexUrl(string uvexUrl, int port, string path)
        {
            return $"{uvexUrl.TrimEnd('/')}:{port}/{path.Trim('/')}/";
        }
    }
    
}