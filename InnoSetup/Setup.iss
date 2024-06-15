#ifndef version
  #define version "0.1.0"
#endif
#define AppName "Shelyak Uvex Bundle"
#define AppPublisher "Shelyak"
#define AppURL "https://www.shelyak.com"
#define AppExeName "Shelyak.Uvex.WebApi.exe"

[Setup]
AppId={{020118E7-FD44-46C9-9566-EE1D8DD84D0B}
AppName={#AppName}
AppVerName={#AppName} {#version}
OutputDir=.\
OutputBaseFilename={#AppName} Setup v{#version}
DefaultDirName={tmp}
Uninstallable=no
DisableDirPage=yes

[Files]
Source: "../Shelyak.Uvex.Web/bin/Release/net8.0/win-x64/publish/InnoSetup/*.exe"; DestDir: "{tmp}"; Flags: nocompression deleteafterinstall
Source: "../Shelyak.Uvex.Ascom/bin/Release/InnoSetup/*.exe"; DestDir: "{tmp}"; Flags: nocompression deleteafterinstall

[Run]
Filename: "{tmp}\Shelyak Uvex Web Setup v{#version}.exe"; Parameters: "/SILENT"; Flags: waituntilterminated skipifdoesntexist; StatusMsg: "Shelyak Uvex installation. Please Wait..."
Filename: "{tmp}\Shelyak Uvex ASCOM Setup v{#version}.exe"; Parameters: "/SILENT"; Flags: waituntilterminated skipifdoesntexist; StatusMsg: "Shelyak Uvex ASCOM installation. Please wait..."; AfterInstall: TaskKill

[Code]
procedure TaskKill();
var
  ResultCode: Integer;
begin
  Sleep(5000);
  Exec(ExpandConstant('{sys}\taskkill.exe'), '/f /im "Shelyak.Uvex.Web.exe"', ExpandConstant('{sys}'), SW_HIDE, ewWaitUntilTerminated, ResultCode);
end;
