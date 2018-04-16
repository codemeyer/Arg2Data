# Creates a NuGet package from the Arg2Data project and copies the created .nupkg file to the Build\Artifacts folder
#
# Uses the version number (and other properties) from Arg2Data.csproj

dotnet pack "$PSScriptRoot\..\Source\Arg2Data\Arg2Data.csproj" -c Release

Copy-Item -Path "$PSScriptRoot\..\Source\Arg2Data\bin\Release\*.nupkg" -Destination "$PSScriptRoot\Artifacts"
