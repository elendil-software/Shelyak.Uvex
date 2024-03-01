using System;
using ASCOM.ShelyakUvex.Shared;

namespace ASCOM.ShelyakUvex.Focuser
{
    public static class FocuserHardwareSettings
    {
        internal static string uvexApiUrl = UvexApiParameter.defaultBaseUrl;
        internal static int uvexApiPort = Convert.ToInt32(UvexApiParameter.defaultPort);
    }
}