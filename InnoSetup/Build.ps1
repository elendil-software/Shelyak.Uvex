# Define the paths
$uvexAscomPath = "../Shelyak.Uvex.Ascom/"
$uvexAscomOutputPath = $uvexAscomPath + "bin/Release/"

$uvexWebPath = "../Shelyak.Uvex.Web/"
$uvexWebOutputPath = $uvexWebPath + "bin/Release/Publish/"

$msbuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\msbuild.exe"
$innoSetupPath = "C:\Program Files (x86)\Inno Setup 6\ISCC.exe"

# Build Shelyak.Uvex.Ascom
Write-Host "Building Shelyak.Uvex.Ascom..."
& $msbuildPath $uvexAscomPath /p:Configuration=Release /t:Restore,Build

# Run Inno Setup.iss in the Shelyak.Uvex.Ascom build folder
Write-Host "Running Inno Setup for Shelyak.Uvex.Ascom..."
& $innoSetupPath "$($uvexAscomOutputPath)InnoSetup/Setup.iss"

# Publish Shelyak.Uvex.Web
Write-Host "Publishing Shelyak.Uvex.Web..."
dotnet publish $uvexWebPath -c Release -r win-x64 --self-contained true -o $uvexWebOutputPath

# Run Inno Setup.iss in the Shelyak.Uvex.Web publish folder
Write-Host "Running Inno Setup for Shelyak.Uvex.Ascom..."
& $innoSetupPath "$($uvexWebOutputPath)InnoSetup/Setup.iss"


# Run Inno Setup.iss in the same folder as this script
Write-Host "Running Inno Setup in the script's folder..."
& $innoSetupPath "Setup.iss"