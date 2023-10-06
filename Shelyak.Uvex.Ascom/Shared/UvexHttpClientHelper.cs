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

        public static string BuildUvexUrl(string uvexUrlFromConfigDialog)
        {
            if (!uvexUrlFromConfigDialog.Contains(UvexApiParameter.defaultApiPath.Trim('/')))
            {
                return $"{uvexUrlFromConfigDialog.TrimEnd('/')}{UvexApiParameter.defaultApiPath.Trim('/')}/";
            }

            return $"{uvexUrlFromConfigDialog.TrimEnd('/')}/";
        }
    }
    
}