#define MyAppName "Shelyak Uvex"
#define MyAppVersion "0.1.0"
#define MyAppPublisher "Shelyak"
#define MyAppURL "https://www.shelyak.com"
#define MyAppExeName "Shelyak.Uvex.WebApi.exe"

[Setup]
AppId={{020118E7-FD44-46C9-9566-EE1D8DD84D0B}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf64}\Shelyak\Uvex
DisableDirPage=yes
DisableProgramGroupPage=yes
LicenseFile=..\Documentation\License.txt
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=.\
OutputBaseFilename=Shelyak Uvex Setup - {#MyAppVersion}
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\*.json"; DestDir: "{app}"; Flags: ignoreversion onlyifdoesntexist; Excludes: appsettings-uvex.json,appsettings.Development.json,http-client.env.json,libman.json
Source: "..\appsettings-uvex.json"; DestDir: "{autoappdata}\Shelyak\Uvex"; Flags: ignoreversion onlyifdoesntexist 
Source: "..\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\*.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\wwwroot\*"; DestDir: "{app}\wwwroot"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

