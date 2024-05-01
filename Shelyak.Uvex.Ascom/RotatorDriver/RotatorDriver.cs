using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ASCOM.DeviceInterface;
using ASCOM.LocalServer;
using ASCOM.ShelyakUvex.Shared;
using ASCOM.Utilities;

namespace ASCOM.ShelyakUvex.Rotator
{
    /// <summary>
    /// ASCOM Rotator Driver for ShelyakUvex.
    /// </summary>
    [ComVisible(true)]
    [Guid("4cc4e64d-8781-4c86-89d5-9c1aa4c9e10c")]
    [ProgId("ASCOM.ShelyakUvex.Rotator")]
    [ServedClassName("Shelyak Uvex")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Rotator : ReferenceCountedObjectBase, IRotatorV3, IDisposable
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
        public Rotator()
        {
            try
            {
                Attribute attr = Attribute.GetCustomAttribute(GetType(), typeof(ProgIdAttribute));
                DriverProgId = ((ProgIdAttribute)attr).Value ?? "PROGID NOT SET!";

                attr = Attribute.GetCustomAttribute(GetType(), typeof(ServedClassNameAttribute));
                DriverDescription = ((ServedClassNameAttribute)attr).DisplayName ?? "DISPLAY NAME NOT SET!";  

                tl = new TraceLogger("", "ShelyakUvex.Driver");
                SetTraceState();
                
                RotatorHardware.InitialiseHardware();

                LogMessage(nameof(Rotator), "Starting driver initialisation");
                LogMessage(nameof(Rotator), $"ProgID: {DriverProgId}, Description: {DriverDescription}");

                connectedState = false;
                
                LogMessage(nameof(Rotator), "Completed initialisation");
            }
            catch (Exception ex)
            {
                LogMessage(nameof(Rotator), $"Initialisation exception: {ex}");
                MessageBox.Show($"{ex.Message}", "Exception creating ASCOM.ShelyakUvex.Rotator", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Class destructor called automatically by the .NET runtime when the object is finalised in order to release resources that are NOT managed by the .NET runtime.
        /// </summary>
        /// <remarks>See the Dispose(bool disposing) remarks for further information.</remarks>
        ~Rotator()
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
        /// 1) Do not call the RotatorHardware.Dispose() method from this method. Any resources used in the static RotatorHardware class itself, 
        ///    which is shared between all instances of the driver, should be released in the RotatorHardware.Dispose() method as usual. 
        ///    The RotatorHardware.Dispose() method will be called automatically by the local server just before it shuts down.
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

        // PUBLIC COM INTERFACE IRotatorV3 IMPLEMENTATION

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
                    LogMessage(nameof(SetupDialog), "Calling SetupDialog.");
                    RotatorHardware.SetupDialog();
                    LogMessage(nameof(SetupDialog), "Completed.");
                }
            }
            catch (Exception ex)
            {
                LogMessage(nameof(SetupDialog), $"Threw an exception: \r\n{ex}");
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
                    CheckConnected(nameof(SupportedActions));
                    ArrayList actions = RotatorHardware.SupportedActions;
                    LogMessage(nameof(SupportedActions), $"Returning {actions.Count} actions.");
                    return actions;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(SupportedActions), $"Threw an exception: \r\n{ex}");
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
                LogMessage(nameof(Action), $"Calling Action: {actionName} with parameters: {actionParameters}");
                string actionResponse = RotatorHardware.Action(actionName, actionParameters);
                LogMessage(nameof(Action), "Completed.");
                return actionResponse;
            }
            catch (Exception ex)
            {
                LogMessage(nameof(Action), $"Threw an exception: \r\n{ex}");
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
                LogMessage(nameof(CommandBlind), $"Calling method - Command: {command}, Raw: {raw}");
                RotatorHardware.CommandBlind(command, raw);
                LogMessage(nameof(CommandBlind), "Completed.");
            }
            catch (Exception ex)
            {
                LogMessage(nameof(CommandBlind), $"Command: {command}, Raw: {raw} threw an exception: \r\n{ex}");
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
                LogMessage(nameof(CommandBool), $"Calling method - Command: {command}, Raw: {raw}");
                bool commandBoolResponse = RotatorHardware.CommandBool(command, raw);
                LogMessage(nameof(CommandBool), $"Returning: {commandBoolResponse}.");
                return commandBoolResponse;
            }
            catch (Exception ex)
            {
                LogMessage(nameof(CommandBool), $"Command: {command}, Raw: {raw} threw an exception: \r\n{ex}");
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
                LogMessage(nameof(CommandString), $"Calling method - Command: {command}, Raw: {raw}");
                string commandStringResponse = RotatorHardware.CommandString(command, raw);
                LogMessage(nameof(CommandString), $"Returning: {commandStringResponse}.");
                return commandStringResponse;
            }
            catch (Exception ex)
            {
                LogMessage(nameof(CommandString), $"Command: {command}, Raw: {raw} threw an exception: \r\n{ex}");
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
                    // Returns the driver's connection state rather than the local server's connected state, which could be different because there may be other client connections still active.
                    LogMessage(nameof(Connected) + " Get", connectedState.ToString());
                    return connectedState;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(Connected) + " Get", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
            set
            {
                try
                {
                    if (value == connectedState)
                    {
                        LogMessage(nameof(Connected) + " Set", "Device already connected, ignoring Connected Set = true");
                        return;
                    }
                    
                    using (ComPortChecker comPortChecker = new ComPortChecker(RotatorHardwareSettings.uvexApiUrl, RotatorHardwareSettings.uvexApiPort))
                    {
                        if (!comPortChecker.CheckConnection())
                        {
                            throw new NotConnectedException("The focuser is not connected to the hardware");
                        }
                    }

                    if (value)
                    {
                        connectedState = true;
                        LogMessage(nameof(Connected) + " Set", "Connecting to device");
                        RotatorHardware.Connected = true;
                    }
                    else
                    {
                        connectedState = false;
                        LogMessage(nameof(Connected) + " Set", "Disconnecting from device");
                        RotatorHardware.Connected = false;
                    }
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(Connected) + " Set", $"Threw an exception: \r\n{ex}");
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
                    CheckConnected(nameof(Description));
                    string description = RotatorHardware.Description;
                    LogMessage(nameof(Description), description);
                    return description;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(Description), $"Threw an exception: \r\n{ex}");
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
                    string driverInfo = RotatorHardware.DriverInfo;
                    LogMessage(nameof(DriverInfo), driverInfo);
                    return driverInfo;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(DriverInfo), $"Threw an exception: \r\n{ex}");
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
                    string driverVersion = RotatorHardware.DriverVersion;
                    LogMessage(nameof(DriverVersion), driverVersion);
                    return driverVersion;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(DriverVersion), $"Threw an exception: \r\n{ex}");
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
                    short interfaceVersion = RotatorHardware.InterfaceVersion;
                    LogMessage(nameof(InterfaceVersion), interfaceVersion.ToString());
                    return interfaceVersion;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(InterfaceVersion), $"Threw an exception: \r\n{ex}");
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
                    string name = RotatorHardware.Name;
                    LogMessage(nameof(Name) + " Get", name);
                    return name;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(Name), $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        #endregion

        #region IRotator Implementation

        /// <summary>
        /// Indicates whether the Rotator supports the <see cref="Reverse" /> method.
        /// </summary>
        /// <returns>
        /// True if the Rotator supports the <see cref="Reverse" /> method.
        /// </returns>
        public bool CanReverse
        {
            get
            {
                try
                {
                    CheckConnected(nameof(CanReverse));
                    bool canReverse = RotatorHardware.CanReverse;
                    LogMessage(nameof(CanReverse), canReverse.ToString());
                    return canReverse;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(CanReverse), $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Immediately stop any Rotator motion due to a previous <see cref="Move">Move</see> or <see cref="MoveAbsolute">MoveAbsolute</see> method call.
        /// </summary>
        public void Halt()
        {
            try
            {
                CheckConnected(nameof(Halt));
                LogMessage(nameof(Halt), "Calling method.");
                RotatorHardware.Halt();
                LogMessage(nameof(Halt), "Completed.");
            }
            catch (Exception ex)
            {
                LogMessage(nameof(Halt), $"Threw an exception: \r\n{ex}");
                throw;
            }
        }

        /// <summary>
        /// Indicates whether the rotator is currently moving
        /// </summary>
        /// <returns>True if the Rotator is moving to a new position. False if the Rotator is stationary.</returns>
        public bool IsMoving
        {
            get
            {
                try
                {
                    CheckConnected(nameof(IsMoving));
                    bool isMoving = RotatorHardware.IsMoving;
                    LogMessage(nameof(IsMoving), isMoving.ToString());
                    return isMoving;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(IsMoving), $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Causes the rotator to move Position degrees relative to the current <see cref="Position" /> value.
        /// </summary>
        /// <param name="position">Relative position to move in degrees from current <see cref="Position" />.</param>
        public void Move(float position)
        {
            try
            {
                CheckConnected(nameof(Move));
                LogMessage(nameof(Move), "Calling method.");
                RotatorHardware.Move(position);
                LogMessage(nameof(Move), "Completed.");
            }
            catch (Exception ex)
            {
                LogMessage(nameof(Move), $"Threw an exception: \r\n{ex}");
                throw;
            }
        }


        /// <summary>
        /// Causes the rotator to move the absolute position of <see cref="Position" /> degrees.
        /// </summary>
        /// <param name="position">Absolute position in degrees.</param>
        public void MoveAbsolute(float position)
        {
            try
            {
                CheckConnected(nameof(MoveAbsolute));
                LogMessage(nameof(MoveAbsolute), "Calling method.");
                RotatorHardware.MoveAbsolute(position);
                LogMessage(nameof(MoveAbsolute), "Completed.");
            }
            catch (Exception ex)
            {
                LogMessage(nameof(MoveAbsolute), $"Threw an exception: \r\n{ex}");
                throw;
            }
        }

        /// <summary>
        /// Current instantaneous Rotator position, allowing for any sync offset, in degrees.
        /// </summary>
        public float Position
        {
            get
            {
                try
                {
                    CheckConnected(nameof(Position));
                    float position = RotatorHardware.Position;
                    LogMessage(nameof(Position), position.ToString());
                    return position;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(Position), $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Sets or Returns the rotator’s Reverse state.
        /// </summary>
        public bool Reverse
        {
            get
            {
                try
                {
                    CheckConnected(nameof(Reverse) + " Get");
                    bool canReverse = RotatorHardware.Reverse;
                    LogMessage(nameof(Reverse) + " Get", canReverse.ToString());
                    return canReverse;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(Reverse) + " Get", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
            set
            {
                try
                {
                    CheckConnected(nameof(Reverse) + " Set");
                    LogMessage(nameof(Reverse) + " Set", value.ToString());
                    RotatorHardware.Reverse = value;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(Reverse) + " Set", $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// The minimum StepSize, in degrees.
        /// </summary>
        public float StepSize
        {
            get
            {
                try
                {
                    CheckConnected(nameof(StepSize));
                    float stepSize = RotatorHardware.StepSize;
                    LogMessage(nameof(StepSize), stepSize.ToString());
                    return stepSize;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(StepSize), $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// The destination position angle for Move() and MoveAbsolute().
        /// </summary>
        public float TargetPosition
        {
            get
            {
                try
                {
                    CheckConnected(nameof(TargetPosition));
                    float targetPosition = RotatorHardware.TargetPosition;
                    LogMessage(nameof(TargetPosition), targetPosition.ToString());
                    return targetPosition;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(TargetPosition), $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        // IRotatorV3 methods

        /// <summary>
        /// This returns the raw mechanical position of the rotator in degrees.
        /// </summary>
        public float MechanicalPosition
        {
            get
            {
                try
                {
                    CheckConnected(nameof(MechanicalPosition));
                    float mechanicalPosition = RotatorHardware.MechanicalPosition;
                    LogMessage(nameof(MechanicalPosition), mechanicalPosition.ToString());
                    return mechanicalPosition;
                }
                catch (Exception ex)
                {
                    LogMessage(nameof(MechanicalPosition), $"Threw an exception: \r\n{ex}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Moves the rotator to the specified mechanical angle. 
        /// </summary>
        /// <param name="position">Mechanical rotator position angle.</param>
        public void MoveMechanical(float position)
        {
            try
            {
                CheckConnected(nameof(MoveMechanical));
                LogMessage(nameof(MoveMechanical), "Calling method.");
                RotatorHardware.MoveMechanical(position);
                LogMessage(nameof(MoveMechanical), "Completed.");
            }
            catch (Exception ex)
            {
                LogMessage(nameof(MoveMechanical), $"Threw an exception: \r\n{ex}");
                throw;
            }
        }

        /// <summary>
        /// Syncs the rotator to the specified position angle without moving it. 
        /// </summary>
        /// <param name="position">Synchronised rotator position angle.</param>
        public void Sync(float position)
        {
            try
            {
                CheckConnected(nameof(Sync));
                LogMessage(nameof(Sync), "Calling method.");
                RotatorHardware.Sync(position);
                LogMessage(nameof(Sync), "Completed.");
            }
            catch (Exception ex)
            {
                LogMessage(nameof(Sync), $"Threw an exception: \r\n{ex}");
                throw;
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

            RotatorHardware.LogMessage(identifier, message);
        }

        /// <summary>
        /// Read the trace state from the driver's Profile and enable / disable the trace log accordingly.
        /// </summary>
        private void SetTraceState()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Rotator";
                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, RotatorHardware.traceStateProfileName, string.Empty, RotatorHardware.traceStateDefault));
            }
        }

        #endregion
    }
}
