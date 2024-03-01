using System;
using ASCOM.ShelyakUvex.Shared;

namespace ASCOM.ShelyakUvex.FilterWheel
{
    public static class FilterWheelHardwareSettings
    {
        internal static string uvexApiUrl = UvexApiParameter.defaultBaseUrl;
        internal static int uvexApiPort = Convert.ToInt32(UvexApiParameter.defaultPort);
    }
}