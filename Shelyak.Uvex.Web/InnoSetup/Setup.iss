#ifndef version
  #define version "0.1.0"
#endif
#define AppName "Shelyak Uvex Web"
#define AppPublisher "Shelyak"
#define AppURL "https://www.shelyak.com"
#define AppExeName "Shelyak.Uvex.Web.exe"

[Setup]
AppId={{020118E7-FD44-46C9-9566-EE1D8DD84D0B}
AppName={#AppName}
AppVerName={#AppName} {#version}
AppPublisher={#AppPublisher}
AppPublisherURL={#AppURL}
AppSupportURL={#AppURL}
AppUpdatesURL={#AppURL}
DefaultDirName={autopf64}\Shelyak\Uvex
DefaultGroupName=Shelyak
DisableDirPage=yes
DisableProgramGroupPage=yes
LicenseFile=..\Documentation\LICENSE.md
OutputDir=.\
OutputBaseFilename=Shelyak Uvex Web Setup v{#version}
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
Source: "..\*.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\*.json"; DestDir: "{app}"; Flags: replacesameversion onlyifdoesntexist; Excludes: appsettings-uvex.json,*.Development.json,http-client.env.json,libman.json
Source: "..\appsettings-uvex.json"; DestDir: "{autoappdata}\Shelyak\Uvex"; Permissions: users-modify; Flags: replacesameversion onlyifdoesntexist 
Source: "..\*.dll"; DestDir: "{app}"; Flags: replacesameversion
Source: "..\fr\*.dll"; DestDir: "{app}\fr\"; Flags: replacesameversion
Source: "..\*.xml"; DestDir: "{app}"; Flags: replacesameversion
Source: "..\wwwroot\*"; DestDir: "{app}\wwwroot"; Flags: replacesameversion  
Source: "..\wwwroot\img\*"; DestDir: "{app}\wwwroot\img"; Flags: replacesameversion  
Source: "..\wwwroot\_content\*"; DestDir: "{app}\wwwroot\_content\"; Flags: replacesameversion recursesubdirs  
Source: "..\wwwroot\lib\*.min.js"; DestDir: "{app}\wwwroot\lib\"; Flags: replacesameversion recursesubdirs;
Source: "..\wwwroot\lib\*.min.css"; DestDir: "{app}\wwwroot\lib\"; Flags: replacesameversion recursesubdirs;
Source: "..\wwwroot\lib\*.woff"; DestDir: "{app}\wwwroot\lib\"; Flags: replacesameversion recursesubdirs;
Source: "..\wwwroot\lib\*.woff2"; DestDir: "{app}\wwwroot\lib\"; Flags: replacesameversion recursesubdirs;
Source: "..\Documentation\*"; DestDir: "{app}\Documentation\"; Flags: replacesameversion;

[InstallDelete]
Type: files; Name: "{app}\Shelyak.Uvex.Web.exe"
Type: files; Name: "{app}\createdump.exe"
Type: files; Name: "{app}\*.json"
Type: files; Name: "{app}\*.dll"
Type: files; Name: "{app}\*.xml"
Type: filesandordirs; Name: "{app}\fr"
Type: filesandordirs; Name: "{app}\wwwroot"
Type: filesandordirs; Name: "{app}\Documentation"


[Icons]
;Name: "{autoprograms}\{#AppPublisher}\{#AppName}"; Filename: "{app}\{#AppExeName}"
Name: "{group}\{#AppName}"; Filename: "{app}\{#AppExeName}";
Name: "{group}\Read Me"; Filename: "{app}\Documentation\Readme.txt";
Name: "{group}\License"; Filename: "{app}\Documentation\License.txt";
Name: "{autodesktop}\{#AppName}"; Filename: "{app}\{#AppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(AppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

