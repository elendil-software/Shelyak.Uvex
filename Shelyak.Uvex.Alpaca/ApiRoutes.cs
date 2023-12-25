namespace Shelyak.Uvex.Alpaca
{
    public class ApiRoutes
    {
        #region Device
        
        public const string DeviceName = "devicename";
        public const string SoftwareVersion = "softwareversion";
        public const string ProtocolVersion = "protocolversion";
        public const string Temperature = "temperature";
        public const string Humidity = "humidity";
        
        #endregion
        
        #region Grating
        
        public const string GratingId = "gratingid";
        public const string GratingAngle = "gratingangle";
        public const string StopGratingAngle = "stopgratingangle";
        public const string CalibrateGratingAngle = "calibrategratingangle";
        public const string GratingAngleMax = "gratinganglemax";
        public const string GratingAngleMin = "gratinganglemin";
        public const string GratingAnglePrec = "gratingangleprec";
        public const string GratingWaveLength = "gratingwavelength";
        public const string StopGratingWaveLength = "stopgratingwavelength";
        public const string CalibrateGratingWaveLength = "calibrategratingwavelength";
        public const string GratingWaveLengthMax = "gratingwavelengthmax";
        public const string GratingWaveLengthMin = "gratingwavelengthmin";
        public const string GratingWaveLengthPrec = "gratingwavelengthprec";
        public const string GratingDensity = "gratingdensity";
        
        #endregion
        
        #region Slit
        
        public const string SlitId = "slitid";
        public const string SlitWidth = "slitwidth";
        public const string SlitAngle = "slitangle";
        
        #endregion
        
        #region Focus
        
        public const string FocusPosition = "focusposition";
        public const string StopFocusPosition = "stopfocusposition";
        public const string CalibrateFocusPosition = "calibratefocusposition";
        public const string FocusPositionMax = "focuspositionmax";
        public const string FocusPositionMin = "focuspositionmin";
        public const string FocusPositionPrec = "focuspositionprec";
        
        #endregion
        
        #region Light Source
        
        public const string LightSource = "lightsource";
        
        #endregion
    }
}