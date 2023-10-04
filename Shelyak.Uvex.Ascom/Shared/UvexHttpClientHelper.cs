using System;
using System.Net.Http;
using ASCOM.ShelyakUvex.Focuser;

namespace ASCOM.ShelyakUvex.Shared
{
    internal static class UvexHttpClientHelper
    {
        public static UvexHttpClient CreateUvexHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(UvexApiParameter.Url);
            return new UvexHttpClient(httpClient, FocuserHardware.tl);
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