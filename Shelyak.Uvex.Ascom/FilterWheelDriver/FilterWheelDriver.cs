using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ASCOM.DeviceInterface;
using ASCOM.LocalServer;
using ASCOM.ShelyakUvex.Shared;
using ASCOM.Utilities;

namespace ASCOM.ShelyakUvex.FilterWheel
{
    /// <summary>
    /// ASCOM FilterWheel Driver for ShelyakUvex.
    /// </summary>
    [ComVisible(true)]
    [Guid("2629c259-4a2a-48f0-9356-0ed772e17431")]
    [ProgId("ASCOM.ShelyakUvex.FilterWheel")]
    [ServedClassName("Shelyak Uvex")]
    [ClassInterface(ClassInterfaceType.None)]
    public class FilterWheel : ReferenceCountedObjectBase, IFilterWheelV2, IDisposable
    {
        internal static string DriverProgId;
        internal static string DriverDescription;
        internal bool connectedState;
        internal TraceLogger tl;
        private bool disposedValue;

        #region Initialisation and Dispose

        /// <summary>
        /// Initializes a new instance of the <see cref="ShelyakUvex"/> class. Must be public to successfully register for COM.
        /// </summary>
        public FilterWheel()
        {
            try
            {
                Attribute attr = Attribute.GetCustomAttribute(GetType(), typeof(ProgIdAttribute));
                DriverProgId = ((ProgIdAttribute)attr).Value ?? "PROGID NOT SET!";
                
                attr = Attribute.GetCustomAttribute(GetType(), typeof(ServedClassNameAttribute));
                DriverDescription = ((ServedClassNameAttribute)attr).DisplayName ?? "DISPLAY NAME NOT SET!";

                //TODO check if driver type is required for the log file type 
                tl = new TraceLogger("", "ShelyakUvex.Driver");
                SetTraceState();
                
                FilterWheelHardware.InitialiseHardware();

                LogMessage("FilterWheel", "Starting driver initialisation");
                LogMessage("FilterWheel", $"ProgID: {DriverProgId}, Description: {DriverDescription}");

                connectedState = false;
                
                LogMessage("FilterWheel", "Completed initialisation");
            }
            catch (Exception ex)
            {
                LogMessage("FilterWheel", $"Initialisation exception: {ex}");
                MessageBox.Show($"{ex.Message}", "Exception creating ASCOM.ShelyakUvex.FilterWheel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Class destructor called automatically by the .NET runtime when the object is finalised in order to release resources that are NOT managed by the .NET runtime.
        /// </summary>
        /// <remarks>See the Dispose(bool disposing) remarks for further information.</remarks>
        ~FilterWheel()
        {
            Dispose(false);
        }

        /// <summary>
        /// Deterministically dispose of any managed and unmanaged resources used in this instance of the driver.
        /// </summary>
        /// <remarks>
        /// Do not dispose of items in this method, put clean-up code in the 'Dispose(bool disposing)' method instead.
        /// </remarks>
        public void Dispose()
        {
            Dispose(disposing: true);
            // Do not add GC.SuppressFinalize(this); here because it breaks the ReferenceCountedObjectBase COM connection counting mechanic
        }

        /// <summary>
        /// Dispose of large or scarce resources created or used within this driver file
        /// </summary>
        /// <remarks>
        /// The purpose of this method is to enable you to release finite system resources back to the operating system as soon as possible, so that other applications work as effectively as possible.
        ///
        /// NOTES
        /// 1) Do not call the FilterWheelHardware.Dispose() method from this method. Any resources used in the static FilterWheelHardware class itself, 
        ///    which is shared between all instances of the driver, should be released in the FilterWheelHardware.Dispose() method as usual. 
        ///    The FilterWheelHardware.Dispose() method will be called automatically by the local server just before it shuts down.
        /// 2) You do not need to release every .NET resource you use in your driver because the .NET runtime is very effective at reclaiming these resources. 
        /// 3) Strong candidates for release here are:
        ///     a) Objects that have a large memory footprint (> 1Mb) such as images
        ///     b) Objects that consume finite OS resources such as file handles, synchronisation object handles, memory allocations requested directly from the operating system (NativeMemory methods) etc.
        /// 4) Please ensure that you do not return exceptions from this method
        /// 5) Be aware that Dispose() can be called more than once:
        ///     a) By the client application
        ///     b) Automatically, by the .NET runtime during finalisation
        /// 6) Because of 5) above, you should make sure that your code is tolerant of multiple calls.    
        /// </remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        if (!(tl is null))
                        {
                            tl.Enabled = false;
                            tl.Dispose();
                            tl = null;
                        }
                    }
                    catch (Exception)
                    {
                        // Any exception is not re-thrown because Microsoft's best practice says not to return exceptions from the Dispose method. 
                    }
                }
                
                disposedValue = true;
            }
        }

        #endregion

        // PUBLIC COM INTERFACE IFilterWheelV2 IMPLEMENTATION

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialogue form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            try
            {
                if (connectedState)
                {
                    MessageBox.Show("Already connected, just press OK");
                }
                else
                {
                    LogMessage("SetupDialog", "Calling SetupDialog.");
                    FilterWheelHardware.SetupDialog();
                    LogMessage("SetupDialog", "Completed.");
                }
            }
            catch (Exception ex)
            {
                LogMessage("SetupDialog", $"Threw an exception: \r\n{ex}");
                throw;
            }
        }

        /// <summary>Returns the list of custom action names supported by this driver.</summary>
        /// <value>An ArrayList of strings (SafeArray collection) containing the names of supported actions.</value>
        public ArrayList SupportedActions
        {
            get
            {
                try
                {
                    CheckConnected("SupportedActions");
                    ArrayList actions = FilterWheelHardware.SupportedActions;
                    LogMessage("SupportedActions", $"Returning {actions.Count} actions.");
                    return actions;
                }
                catch (Exception ex)
                {
                    LogMessage("SupportedActions", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>Invokes the specified device-specific custom action.</summary>
        /// <param name="actionName">A well known name agreed by interested parties that represents the action to be carried out.</param>
        /// <param name="actionParameters">List of required parameters or an <see cref="String.Empty">Empty String</see> if none are required.</param>
        /// <returns>A string response. The meaning of returned strings is set by the driver author.
        /// <para>Suppose filter wheels start to appear with automatic wheel changers; new actions could be <c>QueryWheels</c> and <c>SelectWheel</c>. The former returning a formatted list
        /// of wheel names and the second taking a wheel name and making the change, returning appropriate values to indicate success or failure.</para>
        /// </returns>
        public string Action(string actionName, string actionParameters)
        {
            try
            {
                CheckConnected($"Action {actionName} - {actionParameters}");
                LogMessage("", $"Calling Action: {actionName} with parameters: {actionParameters}");
                string actionResponse = FilterWheelHardware.Action(actionName, actionParameters);
                LogMessage("Action", "Completed.");
                return actionResponse;
            }
            catch (Exception ex)
            {
                LogMessage("Action", $"Threw an exception: \r\n{ex}");
                throw;
            }
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
        public void CommandBlind(string command, bool raw)
        {
            try
            {
                CheckConnected($"CommandBlind: {command}, Raw: {raw}");
                LogMessage("CommandBlind", $"Calling method - Command: {command}, Raw: {raw}");
                FilterWheelHardware.CommandBlind(command, raw);
                LogMessage("CommandBlind", "Completed.");
            }
            catch (Exception ex)
            {
                LogMessage("CommandBlind", $"Command: {command}, Raw: {raw} threw an exception: \r\n{ex}");
                throw;
            }
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
        public bool CommandBool(string command, bool raw)
        {
            try
            {
                CheckConnected($"CommandBool: {command}, Raw: {raw}");
                LogMessage("CommandBlind", $"Calling method - Command: {command}, Raw: {raw}");
                bool commandBoolResponse = FilterWheelHardware.CommandBool(command, raw);
                LogMessage("CommandBlind", $"Returning: {commandBoolResponse}.");
                return commandBoolResponse;
            }
            catch (Exception ex)
            {
                LogMessage("CommandBool", $"Command: {command}, Raw: {raw} threw an exception: \r\n{ex}");
                throw;
            }
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
        public string CommandString(string command, bool raw)
        {
            try
            {
                CheckConnected($"CommandString: {command}, Raw: {raw}");
                LogMessage("CommandString", $"Calling method - Command: {command}, Raw: {raw}");
                string commandStringResponse = FilterWheelHardware.CommandString(command, raw);
                LogMessage("CommandString", $"Returning: {commandStringResponse}.");
                return commandStringResponse;
            }
            catch (Exception ex)
            {
                LogMessage("CommandString", $"Command: {command}, Raw: {raw} threw an exception: \r\n{ex}");
                throw;
            }
        }

        /// <summary>
        /// Set True to connect to the device hardware. Set False to disconnect from the device hardware.
        /// You can also read the property to check whether it is connected. This reports the current hardware state.
        /// </summary>
        /// <value><c>true</c> if connected to the hardware; otherwise, <c>false</c>.</value>
        public bool Connected
        {
            get
            {
                try
                {
                    LogMessage("Connected Get", connectedState.ToString());
                    return connectedState;
                }
                catch (Exception ex)
                {
                    LogMessage("Connected Get", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
            set
            {
                try
                {
                    if (value == connectedState)
                    {
                        LogMessage("Connected Set", "Device already connected, ignoring Connected Set = true");
                        return;
                    }
                    
                    using (ComPortChecker comPortChecker = new ComPortChecker(FilterWheelHardwareSettings.uvexApiUrl, FilterWheelHardwareSettings.uvexApiPort))
                    {
                        if (!comPortChecker.CheckConnection())
                        {
                            throw new NotConnectedException("The focuser is not connected to the hardware");
                        }
                    }

                    if (value)
                    {
                        connectedState = true;
                        LogMessage("Connected Set", "Connecting to device");
                        FilterWheelHardware.Connected = true;
                    }
                    else
                    {
                        connectedState = false;
                        LogMessage("Connected Set", "Disconnecting from device");
                        FilterWheelHardware.Connected = false;
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("Connected Set", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Returns a description of the device, such as manufacturer and model number. Any ASCII characters may be used.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get
            {
                try
                {
                    CheckConnected("Description");
                    string description = FilterWheelHardware.Description;
                    LogMessage("Description", description);
                    return description;
                }
                catch (Exception ex)
                {
                    LogMessage("Description", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Descriptive and version information about this ASCOM driver.
        /// </summary>
        public string DriverInfo
        {
            get
            {
                try
                {
                    string driverInfo = FilterWheelHardware.DriverInfo;
                    LogMessage("DriverInfo", driverInfo);
                    return driverInfo;
                }
                catch (Exception ex)
                {
                    LogMessage("DriverInfo", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// A string containing only the major and minor version of the driver formatted as 'm.n'.
        /// </summary>
        public string DriverVersion
        {
            get
            {
                try
                {
                    string driverVersion = FilterWheelHardware.DriverVersion;
                    LogMessage("DriverVersion", driverVersion);
                    return driverVersion;
                }
                catch (Exception ex)
                {
                    LogMessage("DriverVersion", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// The interface version number that this device supports.
        /// </summary>
        public short InterfaceVersion
        {
            get
            {
                try
                {
                    short interfaceVersion = FilterWheelHardware.InterfaceVersion;
                    LogMessage("InterfaceVersion", interfaceVersion.ToString());
                    return interfaceVersion;
                }
                catch (Exception ex)
                {
                    LogMessage("InterfaceVersion", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// The short name of the driver, for display purposes
        /// </summary>
        public string Name
        {
            get
            {
                try
                {
                    string name = FilterWheelHardware.Name;
                    LogMessage("Name Get", name);
                    return name;
                }
                catch (Exception ex)
                {
                    LogMessage("Name", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        #endregion

        #region IFilerWheel Implementation

        /// <summary>
        /// Focus offset of each filter in the wheel
        /// </summary>
        public int[] FocusOffsets
        {
            get
            {
                try
                {
                    CheckConnected("FocusOffsets");
                    int[] focusoffsets = FilterWheelHardware.FocusOffsets;
                    LogMessage("FocusOffsets", focusoffsets.ToString());
                    return focusoffsets;
                }
                catch (Exception ex)
                {
                    LogMessage("FocusOffsets", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Name of each filter in the wheel
        /// </summary>
        public string[] Names
        {
            get
            {
                try
                {
                    CheckConnected("Names");
                    string[] names = FilterWheelHardware.Names;
                    LogMessage("Names", names.ToString());
                    return names;
                }
                catch (Exception ex)
                {
                    LogMessage("Names", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Sets or returns the current filter wheel position
        /// </summary>
        public short Position
        {
            get
            {
                try
                {
                    CheckConnected("Position Get");
                    short position = FilterWheelHardware.Position;
                    LogMessage("Position Get", position.ToString());
                    return position;
                }
                catch (Exception ex)
                {
                    LogMessage("Position Get", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
            set
            {
                try
                {
                    CheckConnected("Position Set");
                    LogMessage("Position Set", value.ToString());
                    FilterWheelHardware.Position = value;
                }
                catch (Exception ex)
                {
                    LogMessage("Position Set", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        #endregion

        #region Private properties and methods

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!connectedState)
            {
                throw new NotConnectedException($"{DriverDescription} ({DriverProgId}) is not connected: {message}");
            }
        }

        /// <summary>
        /// Log helper function that writes to the driver or local server loggers as required
        /// </summary>
        /// <param name="identifier">Identifier such as method name</param>
        /// <param name="message">Message to be logged.</param>
        private void LogMessage(string identifier, string message)
        {
            if (tl != null)
            {
                tl.LogMessageCrLf(identifier, message);
            }
            
            FilterWheelHardware.LogMessage(identifier, message);
        }

        /// <summary>
        /// Read the trace state from the driver's Profile and enable / disable the trace log accordingly.
        /// </summary>
        private void SetTraceState()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "FilterWheel";
                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, FilterWheelHardware.traceStateProfileName, string.Empty, FilterWheelHardware.traceStateDefault));
            }
        }

        #endregion
    }
}
