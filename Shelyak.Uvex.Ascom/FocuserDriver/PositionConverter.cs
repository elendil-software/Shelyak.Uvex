namespace ASCOM.ShelyakUvex.FocuserDriver
{
    public static class PositionConverterExtensions
    {
        public static float ToUvexPosition(this int value)
        {
            return value / 1000f;
        }
        
        public static int ToAscomPosition(this float value)
        {
            return (int) (value * 1000);
        }
    }
}