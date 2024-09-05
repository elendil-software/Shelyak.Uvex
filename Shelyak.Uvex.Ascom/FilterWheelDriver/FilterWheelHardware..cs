using System;
using System.Collections;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ASCOM.ShelyakUvex.Shared;
using ASCOM.Utilities;
using Shelyak.Usis.Enums;

namespace ASCOM.ShelyakUvex.FilterWheel
{
    /// <summary>
    /// ASCOM FilterWheel hardware class for ShelyakUvex.
    /// </summary>
    [HardwareClass]
    internal static class FilterWheelHardware
    {
        internal const string traceStateProfileName = "Trace Level";
        internal const string traceStateDefault = "true";

        private static string DriverProgId = "";
        private static string DriverDescription = "";
        private static bool connectedState;
        private static bool runOnce;
        internal static TraceLogger tl;

        private static UvexHttpClient _uvexHttpClient;
        
        /// <summary>
        /// Initializes a new instance of the device Hardware class.
        /// </summary>
        static FilterWheelHardware()
        {
            try
            {
                tl = new TraceLogger("", "ShelyakUvex.Hardware");
                DriverProgId = FilterWheel.DriverProgId;
                ReadProfile();
                LogMessage(nameof(FilterWheelHardware), "Static initialiser completed.");
            }
            catch (Exception ex)
            {
                try { LogMessage(nameof(FilterWheelHardware), $"Initialisation exception: {ex}"); } catch { }
                MessageBox.Show($"{ex.Message}", "Exception creating ASCOM.ShelyakUvex.FilterWheel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// Place device initialisation code here
        /// </summary>
        /// <remarks>Called every time a new instance of the driver is created.</remarks>
        internal static void InitialiseHardware()
        {
            LogMessage(nameof(InitialiseHardware), "Start.");

            // Make sure that "one off" activities are only undertaken once
            if (runOnce == false)
            {
                _uvexHttpClient = UvexHttpClientHelper.CreateUvexHttpClient(UvexHttpClientHelper.BuildUvexUrl(FilterWheelHardwareSettings.uvexApiUrl, FilterWheelHardwareSettings.uvexApiPort, UvexApiParameter.defaultApiPath));
                
                LogMessage(nameof(InitialiseHardware), "Starting one-off initialisation.");

                DriverDescription = FilterWheel.DriverDescription;

                LogMessage(nameof(InitialiseHardware), $"ProgID: {DriverProgId}, Description: {DriverDescription}");

                connectedState = false;


                LogMessage(nameof(InitialiseHardware), "Completed basic initialisation");
                
                LogMessage(nameof(InitialiseHardware), "One-off initialisation complete.");
                runOnce = true;
            }
        }

        // PUBLIC COM INTERFACE IFilterWheelV2 IMPLEMENTATION

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
                LogMessage(nameof(SupportedActions) + " Get", "Returning empty ArrayList");
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
            LogMessage(nameof(Action), $"Action {actionName}, parameters {actionParameters} is not implemented");
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
            CheckConnected(nameof(CommandBlind));
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
            CheckConnected(nameof(CommandBool)); 
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
            CheckConnected(nameof(CommandString));
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
            try { LogMessage(nameof(Dispose), "Disposing of assets and closing down."); } catch { }

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
                LogMessage(nameof(Connected), $"Get {IsConnected}");
                return IsConnected;
            }
            set
            {
                LogMessage(nameof(Connected), $"Set {value}");
                if (value == IsConnected)
                {
                    return;
                }

                using (ComPortChecker comPortChecker = new ComPortChecker(FilterWheelHardwareSettings.uvexApiUrl, FilterWheelHardwareSettings.uvexApiPort))
                {
                    if (!comPortChecker.CheckConnection())
                    {
                        throw new NotConnectedException("Unable to connect to the device. Check that the UVEX is connected to the PC and the UVEX service is started");
                    }
                }
                
                if (value)
                {
                    LogMessage(nameof(Connected) + " Set", $"Connecting to {FilterWheelHardwareSettings.uvexApiUrl}");
                    connectedState = true;
                }
                else
                {
                    LogMessage(nameof(Connected) + " Set", $"Disconnecting from port {FilterWheelHardwareSettings.uvexApiUrl}");
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
                LogMessage(nameof(Description) + " Get", DriverDescription);
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
                LogMessage(nameof(DriverInfo) + " Get", driverInfo);
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
                LogMessage(nameof(DriverVersion) + " Get", driverVersion);
                return driverVersion;
            }
        }

        /// <summary>
        /// The interface version number that this device supports.
        /// </summary>
        public static short InterfaceVersion
        {
            get
            {
                LogMessage(nameof(InterfaceVersion) + " Get", "2");
                return Convert.ToInt16("2");
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
                LogMessage(nameof(Name) + " Get", name);
                return name;
            }
        }

        #endregion

        #region IFilerWheel Implementation
        
        private static string[] fwNames = new string[4] { LightSource.SKY.ToString(), LightSource.FLAT.ToString(), LightSource.CALIB.ToString(), LightSource.DARK.ToString() };
        private static short fwPosition;

        /// <summary>
        /// Focus offset of each filter in the wheel
        /// </summary>
        internal static int[] FocusOffsets
        {
            get
            {
                LogMessage(nameof(FocusOffsets) + " Get", "This device doesn't support FocusOffsets, returning 0 for all filters");
                return new int[4] { 0, 0, 0, 0 };
            }
        }

        /// <summary>
        /// Name of each filter in the wheel
        /// </summary>
        internal static string[] Names
        {
            get
            {
                foreach (string fwName in fwNames)
                {
                    LogMessage(nameof(Names) + " Get", fwName);
                }

                return fwNames;
            }
        }

        /// <summary>
        /// Sets or returns the current filter wheel position
        /// </summary>
        internal static short Position
        {
            get
            {
                LightSource lightSource = _uvexHttpClient.GetLightSource().Value.Value;
                LogMessage(nameof(Position) + " Get", lightSource.ToString());
                return (short)lightSource;
            }
            set
            {
                LogMessage(nameof(Position) + " Set", value.ToString());
                if ((value < 0) || (value > fwNames.Length - 1))
                {
                    LogMessage(nameof(Position), "Throwing InvalidValueException - Position: " + value + ", Range: 0 to " + (fwNames.Length - 1));
                    throw new InvalidValueException("Position", value.ToString(), "0 to " + (fwNames.Length - 1));
                }
                
                _uvexHttpClient.SetLightSource((LightSource)value);
                int count = 0;
                do
                {
                    count++;
                    Thread.Sleep(500);
                } while (count < 10 && _uvexHttpClient.GetLightSource().Value.Value != (LightSource)value);
                
                fwPosition = value;
            }
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
                driverProfile.DeviceType = "FilterWheel";
                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, traceStateProfileName, string.Empty, traceStateDefault));
                FilterWheelHardwareSettings.uvexApiUrl = driverProfile.GetValue(DriverProgId, UvexApiParameter.UvexApiUrlProfileName, string.Empty, UvexApiParameter.defaultBaseUrl);
                FilterWheelHardwareSettings.uvexApiPort = Convert.ToInt32(driverProfile.GetValue(DriverProgId, UvexApiParameter.UvexApiPortProfileName, string.Empty, UvexApiParameter.defaultPort));
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal static void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "FilterWheel";
                driverProfile.WriteValue(DriverProgId, traceStateProfileName, tl.Enabled.ToString());
                driverProfile.WriteValue(DriverProgId, UvexApiParameter.UvexApiUrlProfileName, FilterWheelHardwareSettings.uvexApiUrl);
                driverProfile.WriteValue(DriverProgId, UvexApiParameter.UvexApiPortProfileName, FilterWheelHardwareSettings.uvexApiPort.ToString());
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

