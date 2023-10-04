namespace ASCOM.ShelyakUvex.Focuser
{
    public static class UvexApiParameter
    {
        internal const string UvexApiUrlProfileName = "uvexApiUrl";
        public static string UvexApiUrlDefault => "http://localhost:6562/api/v1/Spectrograph/0/";
        public static string Url { get; set; } = "http://localhost:5284/api/v1/Spectrograph/0/";
    }
}