## Created by Dotnet cli
``` powershell
PS D:\> # powershell core version info list

PS D:\> $PSVersionTable

Name                           Value
----                           -----
PSVersion                      7.0.0
PSEdition                      Core
GitCommitId                    7.0.0
OS                             Microsoft Windows 10.0.19041
Platform                       Win32NT
PSCompatibleVersions           {1.0, 2.0, 3.0, 4.0…}
PSRemotingProtocolVersion      2.3
SerializationVersion           1.1.0.1
WSManStackVersion

PS D:\> # dotnet version info list.

PS D:\> dotnet --info
.NET Core SDK (reflecting any global.json):
 Version:   3.1.202
 Commit:    6ea70c8dca

Runtime Environment:
 OS Name:     Windows
 OS Version:  10.0.19041
 OS Platform: Windows
 RID:         win10-x64
 Base Path:   C:\Program Files\dotnet\sdk\3.1.202\

Host (useful for support):
  Version: 3.1.4
  Commit:  0c2e69caa6

.NET Core SDKs installed:
  2.1.700 [C:\Program Files\dotnet\sdk]
  3.1.202 [C:\Program Files\dotnet\sdk]

.NET Core runtimes installed:
  Microsoft.AspNetCore.All 2.1.11 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.All]
  Microsoft.AspNetCore.All 2.1.18 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.All]
  Microsoft.AspNetCore.App 2.1.11 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 2.1.18 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 3.1.4 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.NETCore.App 2.1.11 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 2.1.18 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 3.1.4 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.WindowsDesktop.App 3.1.4 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]

To install additional .NET Core runtimes or SDKs:
  https://aka.ms/dotnet-download

PS D:\> # Created blank solution

PS D:.\git\BeautifulData-Server\server\models\BDS.CollectData> dotnet new sln

PS D:\> # Created csproj with specified csproj name and output folder. Note, Not created csproj in your target folder Because creating folder when create csproj by dotnet cli.

PS D:.\git\BeautifulData-Server\server\models\BDS.CollectData\src> dotnet new classlib --name BDS.CollectData --output ./src --framework netstandard2.0 --langVersion 8.0 --no-restore

The template "Class library" was created successfully.

PD D:\> # Create test csproj

PS D:.\git\BeautifulData-Server\server\models\BDS.CollectData> dotnet new xunit --name BDS.CollectData.Tests --output ./tests --framework netcoreapp3.1 --enable-pack --no-restore

The template "xUnit Test Project" was created successfully.

PS D:\> # Add csproj to sln.

PS D:.\git\BeautifulData-Server\server\models\BDS.CollectData> dotnet sln add ./src/BDS.CollectData.csproj
Project `src\BDS.CollectData.csproj` added to the solution.

PS D:.\git\BeautifulData-Server\server\models\BDS.CollectData> dotnet sln add ./tests/BDS.CollectData.Tests.csproj

Project `tests\BDS.CollectData.Tests.csproj` added to the solution.

```