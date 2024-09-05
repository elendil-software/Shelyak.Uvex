#ifndef version
  #define version "0.1.0"
#endif
#define AppName "Shelyak Uvex ASCOM Drivers"
#define AppPublisher "Shelyak"
#define AppURL "https://www.shelyak.com"


[Setup]
AppID={{7f9c1d87-6d1e-4553-bf58-97897317bf99}
AppName={#AppName}
AppVerName={#AppName} {#version}
AppPublisher={#AppPublisher}
AppPublisherURL={#AppURL}
AppSupportURL={#AppURL}
AppUpdatesURL={#AppURL}
VersionInfoVersion={#version}
MinVersion=6.1.7601
DefaultDirName="{commonpf32}/Shelyak/ASCOM/Uvex"
DisableDirPage=yes
DisableProgramGroupPage=yes
OutputDir="."
OutputBaseFilename="Shelyak Uvex ASCOM Setup v{#version}"
Compression=lzma
SolidCompression=yes
; Put there by Platform if Driver Installer Support selected
WizardImageFile="WizardImage.bmp"
LicenseFile="../Documentation/LICENSE.md"
; {cf}\ASCOM\Uninstall\Focuser folder created by Platform, always
UninstallFilesDir="{app}\Uninstall"
CloseApplications=force

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Dirs]
Name: "{app}\Uninstall"

[Files]
Source: "../ASCOM.ShelyakUvex.exe"; DestDir: "{app}"; Flags: replacesameversion 
Source: "../ASCOM.ShelyakUvex.exe.config"; DestDir: "{app}"; Flags: replacesameversion 
Source: "../Shelyak.*.dll"; DestDir: "{app}"; Flags: replacesameversion
Source: "../Microsoft.Bcl.AsyncInterfaces.dll"; DestDir: "{app}"; Flags: replacesameversion 
Source: "../System.Buffers.dll"; DestDir: "{app}"; Flags: replacesameversion 
Source: "../System.Memory.dll"; DestDir: "{app}"; Flags: replacesameversion 
Source: "../System.Net.Http.Json.dll"; DestDir: "{app}"; Flags: replacesameversion 
Source: "../System.Numerics.Vectors.dll"; DestDir: "{app}"; Flags: replacesameversion 
Source: "../System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: replacesameversion 
Source: "../System.Text.Encodings.Web.dll"; DestDir: "{app}"; Flags: replacesameversion 
Source: "../System.Text.Json.dll"; DestDir: "{app}"; Flags: replacesameversion 
Source: "../System.Threading.Tasks.Extensions.dll"; DestDir: "{app}"; Flags: replacesameversion 
Source: "../System.ValueTuple.dll"; DestDir: "{app}"; Flags: replacesameversion 

Source: "../Documentation/README.txt"; DestDir: "{app}"; Flags: replacesameversion
Source: "../Documentation/LICENSE.md"; DestDir: "{app}"; Flags: replacesameversion

; Only if driver is .NET
[Run]

; Only for .NET local-server drivers
Filename: "{app}\ASCOM.ShelyakUvex.exe"; Parameters: "/register"



; Only if driver is .NET
[UninstallRun]
; This helps to give a clean uninstall

; Only for .NET local-server drivers
Filename: "{app}\ASCOM.ShelyakUvex.exe"; Parameters: "/unregister"



[Code]
const
   REQUIRED_PLATFORM_VERSION = 6.2;    // Set this to the minimum required ASCOM Platform version for this application

//
// Function to return the ASCOM Platform's version number as a double.
//
function PlatformVersion(): Double;
var
   PlatVerString : String;
begin
   Result := 0.0;  // Initialise the return value in case we can't read the registry
   try
      if RegQueryStringValue(HKEY_LOCAL_MACHINE_32, 'Software\ASCOM','PlatformVersion', PlatVerString) then 
      begin // Successfully read the value from the registry
         Result := StrToFloat(PlatVerString); // Create a double from the X.Y Platform version string
      end;
   except                                                                   
      ShowExceptionMessage;
      Result:= -1.0; // Indicate in the return value that an exception was generated
   end;
end;

//
// Before the installer UI appears, verify that the required ASCOM Platform version is installed.
//
function InitializeSetup(): Boolean;
var
   PlatformVersionNumber : double;
 begin
   Result := FALSE;  // Assume failure
   PlatformVersionNumber := PlatformVersion(); // Get the installed Platform version as a double
   If PlatformVersionNumber >= REQUIRED_PLATFORM_VERSION then	// Check whether we have the minimum required Platform or newer
      Result := TRUE
   else
      if PlatformVersionNumber = 0.0 then
         MsgBox('No ASCOM Platform is installed. Please install Platform ' + Format('%3.1f', [REQUIRED_PLATFORM_VERSION]) + ' or later from https://www.ascom-standards.org', mbCriticalError, MB_OK)
      else 
         MsgBox('ASCOM Platform ' + Format('%3.1f', [REQUIRED_PLATFORM_VERSION]) + ' or later is required, but Platform '+ Format('%3.1f', [PlatformVersionNumber]) + ' is installed. Please install the latest Platform before continuing; you will find it at https://www.ascom-standards.org', mbCriticalError, MB_OK);
end;

// Code to enable the installer to uninstall previous versions of itself when a new version is installed
procedure CurStepChanged(CurStep: TSetupStep);
var
  ResultCode: Integer;
  UninstallExe: String;
  UninstallRegistry: String;
begin
  if (CurStep = ssInstall) then // Install step has started
	begin
      // Create the correct registry location name, which is based on the AppId
      UninstallRegistry := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#SetupSetting("AppId")}' + '_is1');
      // Check whether an extry exists
      if RegQueryStringValue(HKLM, UninstallRegistry, 'UninstallString', UninstallExe) then
        begin // Entry exists and previous version is installed so run its uninstaller quietly after informing the user
          MsgBox('Setup will now remove the previous Shelyak UVEX ASCOM driver version.', mbInformation, MB_OK);
          Exec(RemoveQuotes(UninstallExe), ' /SILENT', '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode);
          sleep(1000);    //Give enough time for the install screen to be repainted before continuing
        end
  end;
end;

