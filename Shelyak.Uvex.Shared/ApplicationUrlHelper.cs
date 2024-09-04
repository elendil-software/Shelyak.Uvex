using System.Text.RegularExpressions;

namespace Shelyak.Uvex.Shared
{
    public static class ApplicationUrlHelper
    {
        public static string GetApplicationUrl(string configUrl)
        {
            configUrl = configUrl ?? UvexConst.DefaultUrl;
            var urlPattern = @"^(?<scheme>http[s]?)://(?<domain>[^:/]+)(:(?<port>\d+))?$";

            var regex = new Regex(urlPattern);
            var match = regex.Match(configUrl);

            if (match.Success && match.Groups["domain"].Value == "*")
            {
                return configUrl.Replace("*", "localhost");
            }

            return configUrl;
        }
    }
}