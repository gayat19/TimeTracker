; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Ideal Time Tracker"
#define MyAppVersion "1,0.0"
#define MyAppPublisher "My Mangalam Inc."
#define MyAppURL "https://www.example.com/"
#define MyAppExeName "IdealTimeTracker.WPF.exe"
#define MyAppAssocName MyAppName + " File"
#define MyAppAssocExt ".myp"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{9E0AC3B6-DD00-4243-95DD-A5036803C12E}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\InnoSetup\x86
OutputBaseFilename=mysetupx86
SetupIconFile=D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\IdealTimeTracker.Desktop\IdealTimeTracker.WPF\Asset\icon.ico
Password=password
Compression=lzma
SolidCompression=yes
WizardStyle=modern
AppMutex=IdealTimeTracker


[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\IdealTimeTracker.Desktop\IdealTimeTracker.WPF\bin\Release\net6.0-windows10.0.19041.0\publish\win-x86\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\IdealTimeTracker.Desktop\IdealTimeTracker.WPF\bin\Release\net6.0-windows10.0.19041.0\publish\win-x86\appsettings.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\IdealTimeTracker.Desktop\IdealTimeTracker.WPF\bin\Release\net6.0-windows10.0.19041.0\publish\win-x86\D3DCompiler_47_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\IdealTimeTracker.Desktop\IdealTimeTracker.WPF\bin\Release\net6.0-windows10.0.19041.0\publish\win-x86\e_sqlite3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\IdealTimeTracker.Desktop\IdealTimeTracker.WPF\bin\Release\net6.0-windows10.0.19041.0\publish\win-x86\IdealTimeTracker.WPF.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\IdealTimeTracker.Desktop\IdealTimeTracker.WPF\bin\Release\net6.0-windows10.0.19041.0\publish\win-x86\IdealTimeTracker.WPF.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\IdealTimeTracker.Desktop\IdealTimeTracker.WPF\bin\Release\net6.0-windows10.0.19041.0\publish\win-x86\PenImc_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\IdealTimeTracker.Desktop\IdealTimeTracker.WPF\bin\Release\net6.0-windows10.0.19041.0\publish\win-x86\PresentationNative_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\IdealTimeTracker.Desktop\IdealTimeTracker.WPF\bin\Release\net6.0-windows10.0.19041.0\publish\win-x86\vcruntime140_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\G3 mam's Project\Ideal time tracker\source\Ideal-time-tracker\IdealTimeTracker.Desktop\IdealTimeTracker.WPF\bin\Release\net6.0-windows10.0.19041.0\publish\win-x86\wpfgfx_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\{#MyAppExeName}\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{commonstartup}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"

[Dirs]                                     
Name: "{app}"; Permissions: users-modify

[UninstallDelete]
Type: filesandordirs; Name: "{app}"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

