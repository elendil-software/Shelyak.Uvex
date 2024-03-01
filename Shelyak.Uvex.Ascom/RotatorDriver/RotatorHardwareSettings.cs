using System;
using ASCOM.ShelyakUvex.Shared;

namespace ASCOM.ShelyakUvex.Rotator
{
    public static class RotatorHardwareSettings
    {
        internal static string uvexApiUrl = UvexApiParameter.defaultBaseUrl;
        internal static int uvexApiPort = Convert.ToInt32(UvexApiParameter.defaultPort);
    }
}