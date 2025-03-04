﻿using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using ASCOM.ShelyakUvex.Shared;
using ASCOM.Utilities;
using Shelyak.Usis.Enums;

namespace ASCOM.ShelyakUvex.FocuserDriver
{
    /// <summary>
    /// ASCOM Focuser hardware class for ShelyakUvex.
    /// </summary>
    [HardwareClass]
    internal static class FocuserHardware
    {
        internal const string traceStateProfileName = "Trace Level";
        internal const string traceStateDefault = "true";

        private static string DriverProgId = "";
        private static string DriverDescription = "";
        private static bool connectedState;
        private static bool runOnce;
        internal static TraceLogger tl;

        /// <summary>
        /// Initializes a new instance of the device Hardware class.
        /// </summary>
        static FocuserHardware()
        {
            try
            {
                tl = new TraceLogger("", "ShelyakUvex.Hardware");
                DriverProgId = Focuser.DriverProgId;
                ReadProfile();
                LogMessage(nameof(FocuserHardware), "Static initialiser completed.");
            }
            catch (Exception ex)
            {
                try { LogMessage(nameof(FocuserHardware), $"Initialisation exception: {ex}"); } catch { }
                MessageBox.Show($"{ex.Message}", "Exception creating ASCOM.ShelyakUvex.Focuser", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _uvexHttpClient = UvexHttpClientHelper.CreateUvexHttpClient(UvexHttpClientHelper.BuildUvexUrl(UvexApiParameter.defaultApiPath));

                LogMessage(nameof(InitialiseHardware), "Starting one-off initialisation.");

                DriverDescription = Focuser.DriverDescription;

                LogMessage(nameof(InitialiseHardware), $"ProgID: {DriverProgId}, Description: {DriverDescription}");

                connectedState = false;

                LogMessage(nameof(InitialiseHardware), "Completed basic initialisation");
                
                LogMessage(nameof(InitialiseHardware), "One-off initialisation complete.");
                runOnce = true;
            }
        }

        // PUBLIC COM INTERFACE IFocuserV3 IMPLEMENTATION

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
        /// <param name="ActionName">A well known name agreed by interested parties that represents the action to be carried out.</param>
        /// <param name="ActionParameters">List of required parameters or an <see cref="string.Empty">Empty String</see> if none are required.</param>
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
                    return;

                using (ComPortChecker comPortChecker = new ComPortChecker(tl))
                {
                    if (!comPortChecker.CheckConnection())
                    {
                        throw new NotConnectedException("Unable to connect to the device. Check that the configured COM port is correct");
                    }
                }
                
                if (value)
                {
                    LogMessage(nameof(Connected) + " Set", "Connected");
                    connectedState = true;
                }
                else
                {
                    LogMessage(nameof(Connected) + " Set", "Disconnected");
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
                LogMessage(nameof(InterfaceVersion) + " Get", "3");
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
                LogMessage(nameof(Name) + " Get", name);
                return name;
            }
        }

        #endregion

        #region IFocuser Implementation

        private static UvexHttpClient _uvexHttpClient;
        private static int _lastTargetPosition;
        private static float _focusPrecision = -1;

        /// <summary>
        /// True if the focuser is capable of absolute position; that is, being commanded to a specific step location.
        /// </summary>
        internal static bool Absolute
        {
            get
            {
                LogMessage(nameof(Absolute) + " Get", true.ToString());
                return true;
            }
        }

        /// <summary>
        /// Immediately stop any focuser motion due to a previous <see cref="Move" /> method call.
        /// </summary>
        internal static void Halt()
        {
            _uvexHttpClient.StopFocus();
            LogMessage(nameof(Halt), "Halt command issued");
        }

        /// <summary>
        /// True if the focuser is currently moving to a new position. False if the focuser is stationary.
        /// </summary>
        internal static bool IsMoving
        {
            get
            {
                bool isMoving = _uvexHttpClient.GetFocusPosition().Value.Status == (int)PropertyAttributeStatus.BUSY;
                LogMessage(nameof(IsMoving) + " Get", isMoving.ToString());
                return isMoving;
            }
        }

        /// <summary>
        /// State of the connection to the focuser.
        /// </summary>
        internal static bool Link
        {
            get => Connected;
            set => Connected = value;
        }

        /// <summary>
        /// Maximum increment size allowed by the focuser;
        /// i.e. the maximum number of steps allowed in one move operation.
        /// </summary>
        internal static int MaxIncrement
        {
            get
            {
                int maxStep = MaxStep;
                LogMessage(nameof(MaxIncrement) + " Get", maxStep.ToString());
                return maxStep;
            }
        }

        /// <summary>
        /// Maximum step position permitted.
        /// </summary>
        internal static int MaxStep
        {
            get
            {
                float maxPosition = _uvexHttpClient.GetFocusPositionMax().Value.Value;
                var maxIncrement = maxPosition.ToAscomPosition();
                LogMessage(nameof(MaxStep) + " Get", maxIncrement.ToString(CultureInfo.InvariantCulture));
                return maxIncrement;
            }
        }

        /// <summary>
        /// Moves the focuser by the specified amount or to the specified position depending on the value of the <see cref="Absolute" /> property.
        /// </summary>
        /// <param name="Position">Step distance or absolute position, depending on the value of the <see cref="Absolute" /> property.</param>
        internal static void Move(int Position)
        {
            _lastTargetPosition = Position;
            var position = Position.ToUvexPosition();
            LogMessage(nameof(Move), position.ToString(CultureInfo.InvariantCulture));
            _uvexHttpClient.MoveFocus(position);
        }

        /// <summary>
        /// Current focuser position, in steps.
        /// </summary>
        internal static int Position
        {
            get
            {
                var position = _uvexHttpClient.GetFocusPosition().Value;
                int focuserPosition;

                if (_lastTargetPosition > 0 && position.Status == (int)PropertyAttributeStatus.OK && Math.Abs(_lastTargetPosition.ToUvexPosition() - position.Value) < GetFocusPrecision())
                {
                    focuserPosition = _lastTargetPosition;
                }
                else
                {
                    focuserPosition = position.Value.ToAscomPosition();
                }
                
                LogMessage(nameof(Position) + " Get", focuserPosition.ToString(CultureInfo.InvariantCulture));
                return focuserPosition;
            }
        }

        private static float GetFocusPrecision()
        {
            if (_focusPrecision < 0)
            {
                _focusPrecision = _uvexHttpClient.GetFocusPositionPrecision().Value.Value;
            }

            return _focusPrecision;
        }


        /// <summary>
        /// Step size (microns) for the focuser.
        /// </summary>
        internal static double StepSize
        {
            get
            {
                LogMessage(nameof(StepSize) + " Get", "Not implemented");
                throw new PropertyNotImplementedException("StepSize", false);
            }
        }

        /// <summary>
        /// The state of temperature compensation mode (if available), else always False.
        /// </summary>
        internal static bool TempComp
        {
            get
            {
                LogMessage(nameof(TempComp) + " Get", false.ToString());
                return false;
            }
            set
            {
                LogMessage(nameof(TempComp) + " Set", "Not implemented");
                throw new PropertyNotImplementedException("TempComp", false);
            }
        }

        /// <summary>
        /// True if focuser has temperature compensation available.
        /// </summary>
        internal static bool TempCompAvailable
        {
            get
            {
                LogMessage(nameof(TempCompAvailable) + " Get", false.ToString());
                return false;
            }
        }

        /// <summary>
        /// Current ambient temperature in degrees Celsius as measured by the focuser.
        /// </summary>
        internal static double Temperature
        {
            get
            {
                float temperature = _uvexHttpClient.GetTemperature().Value.Value;
                LogMessage(nameof(Temperature) + " Get", temperature.ToString(CultureInfo.InvariantCulture));
                return temperature;
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
                driverProfile.DeviceType = "Focuser";
                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, traceStateProfileName, string.Empty, traceStateDefault));
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal static void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Focuser";
                driverProfile.WriteValue(DriverProgId, traceStateProfileName, tl.Enabled.ToString());
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

