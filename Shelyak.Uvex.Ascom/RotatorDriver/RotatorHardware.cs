using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.ShelyakUvex.Shared;
using ASCOM.Utilities;
using Shelyak.Usis.Enums;

namespace ASCOM.ShelyakUvex.Rotator
{
    /// <summary>
    /// ASCOM Rotator hardware class for ShelyakUvex.
    /// </summary>
    [HardwareClass] // Class attribute flag this as a device hardware class that needs to be disposed by the local server when it exits.
    internal static class RotatorHardware
    {
        internal const string traceStateProfileName = "Trace Level";
        internal const string traceStateDefault = "true";

        private static string DriverProgId = "";
        private static string DriverDescription = "";
        private static bool connectedState;
        private static bool runOnce;
        internal static AstroUtils astroUtilities;
        internal static TraceLogger tl;

        private static UvexHttpClient _uvexHttpClient;
        
        /// <summary>
        /// Initializes a new instance of the device Hardware class.
        /// </summary>
        static RotatorHardware()
        {
            try
            {
                tl = new TraceLogger("", "ShelyakUvex.Hardware");

                DriverProgId = Rotator.DriverProgId;
                ReadProfile();

                LogMessage("RotatorHardware", "Static initialiser completed.");
            }
            catch (Exception ex)
            {
                try { LogMessage("RotatorHardware", $"Initialisation exception: {ex}"); } catch { }
                MessageBox.Show($"{ex.Message}", "Exception creating ASCOM.ShelyakUvex.Rotator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// Place device initialisation code here
        /// </summary>
        /// <remarks>Called every time a new instance of the driver is created.</remarks>
        internal static void InitialiseHardware()
        {
            LogMessage("InitialiseHardware", "Start.");

            if (runOnce == false)
            {
                _uvexHttpClient = UvexHttpClientHelper.CreateUvexHttpClient(UvexHttpClientHelper.BuildUvexUrl(RotatorHardwareSettings.uvexApiUrl, RotatorHardwareSettings.uvexApiPort, UvexApiParameter.defaultApiPath));
                
                LogMessage("InitialiseHardware", "Starting one-off initialisation.");

                DriverDescription = Rotator.DriverDescription;

                LogMessage("InitialiseHardware", $"ProgID: {DriverProgId}, Description: {DriverDescription}");

                connectedState = false;
                astroUtilities = new AstroUtils();

                LogMessage("InitialiseHardware", "Completed basic initialisation");
                
                LogMessage("InitialiseHardware", "One-off initialisation complete.");
                runOnce = true;
            }
        }

        // PUBLIC COM INTERFACE IRotatorV3 IMPLEMENTATION

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialogue form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public static void SetupDialog()
        {
            if (IsConnected)
                MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm(tl))
            {
                var result = F.ShowDialog();
                if (result == DialogResult.OK)
                {
                    WriteProfile();
                }
            }
        }

        /// <summary>Returns the list of custom action names supported by this driver.</summary>
        /// <value>An ArrayList of strings (SafeArray collection) containing the names of supported actions.</value>
        public static ArrayList SupportedActions
        {
            get
            {
                LogMessage("SupportedActions Get", "Returning empty ArrayList");
                return new ArrayList();
            }
        }

        /// <summary>Invokes the specified device-specific custom action.</summary>
        /// <param name="actionName">A well known name agreed by interested parties that represents the action to be carried out.</param>
        /// <param name="actionParameters">List of required parameters or an <see cref="string.Empty">Empty String</see> if none are required.</param>
        /// <returns>A string response. The meaning of returned strings is set by the driver author.
        /// <para>Suppose filter wheels start to appear with automatic wheel changers; new actions could be <c>QueryWheels</c> and <c>SelectWheel</c>. The former returning a formatted list
        /// of wheel names and the second taking a wheel name and making the change, returning appropriate values to indicate success or failure.</para>
        /// </returns>
        public static string Action(string actionName, string actionParameters)
        {
            LogMessage("Action", $"Action {actionName}, parameters {actionParameters} is not implemented");
            throw new ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and does not wait for a response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="command">The literal command string to be transmitted.</param>
        /// <param name="raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        public static void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            throw new MethodNotImplementedException($"CommandBlind - Command:{command}, Raw: {raw}.");
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and waits for a boolean response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="command">The literal command string to be transmitted.</param>
        /// <param name="raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        /// <returns>
        /// Returns the interpreted boolean response received from the device.
        /// </returns>
        public static bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            throw new MethodNotImplementedException($"CommandBool - Command:{command}, Raw: {raw}.");
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and waits for a string response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="command">The literal command string to be transmitted.</param>
        /// <param name="raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        /// <returns>
        /// Returns the string response received from the device.
        /// </returns>
        public static string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            throw new MethodNotImplementedException($"CommandString - Command:{command}, Raw: {raw}.");
        }

        /// <summary>
        /// Deterministically release both managed and unmanaged resources that are used by this class.
        /// </summary>
        /// <remarks>
        /// 
        /// Do not call this method from the Dispose method in your driver class.
        ///
        /// This is because this hardware class is decorated with the <see cref="HardwareClassAttribute"/> attribute and this Dispose() method will be called 
        /// automatically by the  local server executable when it is irretrievably shutting down. This gives you the opportunity to release managed and unmanaged 
        /// resources in a timely fashion and avoid any time delay between local server close down and garbage collection by the .NET runtime.
        ///
        /// For the same reason, do not call the SharedResources.Dispose() method from this method. Any resources used in the static shared resources class
        /// itself should be released in the SharedResources.Dispose() method as usual. The SharedResources.Dispose() method will be called automatically 
        /// by the local server just before it shuts down.
        /// 
        /// </remarks>
        public static void Dispose()
        {
            try { LogMessage("Dispose", "Disposing of assets and closing down."); } catch { }

            try
            {
                _uvexHttpClient?.Dispose();
            }
            catch (Exception e)
            {
                LogMessage(nameof(Dispose), "Error while disposing UvexHttpClient: {0}", e.Message);
            }
            
            try
            {
                tl.Enabled = false;
                tl.Dispose();
                tl = null;
            }
            catch { }

            try
            {
                astroUtilities.Dispose();
                astroUtilities = null;
            }
            catch { }
        }

        /// <summary>
        /// Set True to connect to the device hardware. Set False to disconnect from the device hardware.
        /// You can also read the property to check whether it is connected. This reports the current hardware state.
        /// </summary>
        /// <value><c>true</c> if connected to the hardware; otherwise, <c>false</c>.</value>
        public static bool Connected
        {
            get
            {
                LogMessage("Connected", $"Get {IsConnected}");
                return IsConnected;
            }
            set
            {
                //TODO Add ComPortChecker like in FocuserHardware
                LogMessage("Connected", $"Set {value}");
                if (value == IsConnected)
                    return;

                if (value)
                {
                    LogMessage("Connected Set", $"Connecting to port {RotatorHardwareSettings.uvexApiUrl}");
                    connectedState = true;
                }
                else
                {
                    LogMessage("Connected Set", $"Disconnecting from port {RotatorHardwareSettings.uvexApiUrl}");
                    connectedState = false;
                }
            }
        }

        /// <summary>
        /// Returns a description of the device, such as manufacturer and model number. Any ASCII characters may be used.
        /// </summary>
        /// <value>The description.</value>
        public static string Description
        { 
            get
            {
                LogMessage("Description Get", DriverDescription);
                return DriverDescription;
            }
        }

        /// <summary>
        /// Descriptive and version information about this ASCOM driver.
        /// </summary>
        public static string DriverInfo
        {
            get
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = $"Shelyak UVEX. Version: {version.Major}.{version.Minor}";
                LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        /// <summary>
        /// A string containing only the major and minor version of the driver formatted as 'm.n'.
        /// </summary>
        public static string DriverVersion
        {
            get
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = $"{version.Major}.{version.Minor}";
                LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        /// <summary>
        /// The interface version number that this device supports.
        /// </summary>
        public static short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                LogMessage("InterfaceVersion Get", "3");
                return Convert.ToInt16("3");
            }
        }

        /// <summary>
        /// The short name of the driver, for display purposes
        /// </summary>
        public static string Name
        {
            get
            {
                string name = "Shelyak UVEX";
                LogMessage("Name Get", name);
                return name;
            }
        }

        #endregion

        #region IRotator Implementation

        private static float rotatorPosition;

        /// <summary>
        /// Indicates whether the Rotator supports the <see cref="Reverse" /> method.
        /// </summary>
        /// <returns>
        /// True if the Rotator supports the <see cref="Reverse" /> method.
        /// </returns>
        internal static bool CanReverse
        {
            get
            {
                LogMessage("CanReverse Get", false.ToString());
                return false;
            }
        }

        /// <summary>
        /// Immediately stop any Rotator motion due to a previous <see cref="Move">Move</see> or <see cref="MoveAbsolute">MoveAbsolute</see> method call.
        /// </summary>
        internal static void Halt()
        {
            _uvexHttpClient.StopGratingAngle();
            LogMessage("Halt", "Not implemented");
        }

        /// <summary>
        /// Indicates whether the rotator is currently moving
        /// </summary>
        /// <returns>True if the Rotator is moving to a new position. False if the Rotator is stationary.</returns>
        internal static bool IsMoving
        {
            get
            {
                var gratingAngle = _uvexHttpClient.GetGratingAngle();
                bool isMoving = gratingAngle.Value.Status == (int)PropertyAttributeStatus.BUSY;
                LogMessage("IsMoving Get", false.ToString());

                if (isMoving)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Causes the rotator to move Position degrees relative to the current <see cref="Position" /> value.
        /// </summary>
        /// <param name="Position">Relative position to move in degrees from current <see cref="Position" />.</param>
        internal static void Move(float Position)
        {
            float gratingAngle = _uvexHttpClient.GetGratingAngle().Value.Value;
            float newPosition = (float)astroUtilities.Range(gratingAngle + Position, 0.0, true, 360.0, false);
            LogMessage("Move", $"Move by {Position.ToString()}, new position {newPosition}");
            _uvexHttpClient.SetGratingAngle(newPosition);
        }


        /// <summary>
        /// Causes the rotator to move the absolute position of <see cref="Position" /> degrees.
        /// </summary>
        /// <param name="Position">Absolute position in degrees.</param>
        internal static void MoveAbsolute(float Position)
        {
            LogMessage("MoveAbsolute", $"Move to position {Position.ToString()}");
            
            float newPosition = (float)astroUtilities.Range(Position, 0.0, true, 360.0, false);
            _uvexHttpClient.SetGratingAngle(newPosition);
        }

        /// <summary>
        /// Current instantaneous Rotator position, allowing for any sync offset, in degrees.
        /// </summary>
        internal static float Position
        {
            get
            {
                float gratingAngle = _uvexHttpClient.GetGratingAngle().Value.Value;
                LogMessage("Position Get", gratingAngle.ToString());
                return gratingAngle;
            }
        }

        /// <summary>
        /// Sets or Returns the rotator’s Reverse state.
        /// </summary>
        internal static bool Reverse
        {
            get
            {
                LogMessage("Reverse Get", "Not implemented");
                throw new PropertyNotImplementedException("Reverse", false);
            }
            set
            {
                LogMessage("Reverse Set", "Not implemented");
                throw new PropertyNotImplementedException("Reverse", true);
            }
        }

        /// <summary>
        /// The minimum StepSize, in degrees.
        /// </summary>
        internal static float StepSize
        {
            get
            {
                float stepSize = _uvexHttpClient.GetGratingAnglePrecision().Value.Value;
                LogMessage("StepSize Get", stepSize.ToString());
                return stepSize;
            }
        }

        /// <summary>
        /// The destination position angle for Move() and MoveAbsolute().
        /// </summary>
        internal static float TargetPosition
        {
            get
            {
                LogMessage("TargetPosition Get", rotatorPosition.ToString());
                return rotatorPosition;
            }
        }

        // IRotatorV3 methods

        /// <summary>
        /// This returns the raw mechanical position of the rotator in degrees.
        /// </summary>
        internal static float MechanicalPosition
        {
            get
            {
                float position = Position;
                LogMessage("MechanicalPosition Get", position.ToString());
                return position;
            }
        }

        /// <summary>
        /// Moves the rotator to the specified mechanical angle. 
        /// </summary>
        /// <param name="Position">Mechanical rotator position angle.</param>
        internal static void MoveMechanical(float Position)
        {
            LogMessage("MoveMechanical", Position.ToString());
            MoveAbsolute(Position); 
        }

        /// <summary>
        /// Syncs the rotator to the specified position angle without moving it. 
        /// </summary>
        /// <param name="Position">Synchronised rotator position angle.</param>
        internal static void Sync(float Position)
        {
            LogMessage("Sync", Position.ToString());
            Position = rotatorPosition = (float)astroUtilities.Range(Position, 0.0, true, 360.0, false);
            _uvexHttpClient.CalibrateGratingAngle(Position); 
        }

        #endregion

        #region Private properties and methods

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private static bool IsConnected
        {
            get
            {
                return connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private static void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal static void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Rotator";
                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, traceStateProfileName, string.Empty, traceStateDefault));
                RotatorHardwareSettings.uvexApiUrl = driverProfile.GetValue(DriverProgId, UvexApiParameter.UvexApiUrlProfileName, string.Empty, UvexApiParameter.defaultBaseUrl);
                RotatorHardwareSettings.uvexApiPort = Convert.ToInt32(driverProfile.GetValue(DriverProgId, UvexApiParameter.UvexApiPortProfileName, string.Empty, UvexApiParameter.defaultPort));
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal static void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Rotator";
                driverProfile.WriteValue(DriverProgId, traceStateProfileName, tl.Enabled.ToString());
                driverProfile.WriteValue(DriverProgId, UvexApiParameter.UvexApiUrlProfileName, RotatorHardwareSettings.uvexApiUrl);
                driverProfile.WriteValue(DriverProgId, UvexApiParameter.UvexApiPortProfileName, RotatorHardwareSettings.uvexApiPort.ToString());
            }
        }

        /// <summary>
        /// Log helper function that takes identifier and message strings
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        internal static void LogMessage(string identifier, string message)
        {
            tl.LogMessageCrLf(identifier, message);
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal static void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            LogMessage(identifier, msg);
        }
        #endregion
    }
}

