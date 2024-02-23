#define AppName "Shelyak Uvex"
#define AppVersion "0.1.0"
#define AppPublisher "Shelyak"
#define AppURL "https://www.shelyak.com"
#define AppExeName "Shelyak.Uvex.WebApi.exe"

[Setup]
AppId={{020118E7-FD44-46C9-9566-EE1D8DD84D0B}
AppName={#AppName}
AppVerName={#AppName} {#AppVersion}
OutputDir=.\
OutputBaseFilename={#AppName} Setup v{#AppVersion}
DefaultDirName={tmp}
Uninstallable=no
DisableDirPage=yes

[Files]
Source: "../Shelyak.Uvex.Web/bin/Release/Publish/InnoSetup/*.exe"; DestDir: "{tmp}"; Flags: nocompression deleteafterinstall
Source: "../Shelyak.Uvex.Ascom/bin/Release/InnoSetup/*.exe"; DestDir: "{tmp}"; Flags: nocompression deleteafterinstall

[Run]
Filename: "{tmp}\Shelyak Uvex Web Setup v{#AppVersion}.exe"; Parameters: "/SILENT"; Flags: waituntilterminated skipifdoesntexist; StatusMsg: "Shelyak Uvex installation. Please Wait..."
Filename: "{tmp}\Shelyak Uvex ASCOM Setup v{#AppVersion}.exe"; Parameters: "/SILENT"; Flags: waituntilterminated skipifdoesntexist; StatusMsg: "Shelyak Uvex ASCOM installation. Please wait..."