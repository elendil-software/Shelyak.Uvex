namespace ASCOM.ShelyakUvex.Shared
{
    public static class UvexApiParameter
    {
        internal const string defaultBaseUrl = "http://localhost:6562";
        internal const string defaultApiPath = "/api/v1/Spectrograph/0/";
        internal const string UvexApiUrlProfileName = "uvexApiUrl";
        public static string UvexApiUrlDefault => $"{defaultBaseUrl}{defaultApiPath}";
        public static string Url { get; set; } = UvexApiUrlDefault;
    }
}