param (
    [Parameter(Mandatory)]
    [String]
    $ServicePath,

    [Parameter(Mandatory)]
    [String]
    $PublishCodePath
)
function LoggerInfo([string]$Message) {
    return Write-Host -ForegroundColor Green (Get-Date).ToString('yyyy-MM-dd HH:mm:ss,sss') $message
}

$params = @{
    Name = "BDS"
    BinaryPathName = "$ServicePath\BDS.Runtime.exe"
    DisplayName = "Beautiful Data Service"
    StartupType = "Manual"
    Description = "Get data form web world."
  }
LoggerInfo -Message 'Start Deploy BDS...'

# Check if the script file running with administrator privileges?
$currentPrincipal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
if (!$currentPrincipal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator))
{
    throw 'Script is not run as administrator'
}

$isBDSExists = $true
try 
{
    $server = Get-Service -Name $params.Name -ErrorAction 'Stop'
    LoggerInfo -Message 'Get BDS Service.'  
}
catch
{
    $isBDSExists = $false
    LoggerInfo -Message 'The BDS Service not exists on machine.'
}

if ($isBDSExists) {
    LoggerInfo -Message 'Stop BDS service.'
    Stop-Service -InputObject $server -Force -ErrorAction 'Stop'
    $server = Get-Service -InputObject $server -ErrorAction 'Stop'
    if ($server.Status -ne 'Stopped')
    {
        throw "Can not stop service, Status $($server.Status)" 
    }
    LoggerInfo -Message 'Remove BDS service.'
    Remove-Service -InputObject $server -ErrorAction 'Stop'
}

# Replace service resource.
if (!(Test-Path -Path $ServicePath))
{
    LoggerInfo -Message "Create new path $ServicePath"
    New-Item -Path $ServicePath -ItemType 'directory' -Force -ErrorAction 'Stop'
} 

LoggerInfo -Message "Remove items of the service directory. $ServicePath"
Remove-Item -Path "$ServicePath\*" -Exclude @('Resources') -Recurse -Force -ErrorAction 'Stop'

LoggerInfo -Message "Copy items of the publish directory($PublishCodePath) to the service directory $ServicePath......"
Copy-Item -Path "$PublishCodePath\*" -Destination "$ServicePath" -Recurse -Force -ErrorAction 'Stop'
LoggerInfo -Message "Copy item completed."



LoggerInfo -Message 'Create BDS service'
New-Service @params
LoggerInfo -Message 'Start up BDS service'
$service = Start-Service -Name $params.Name

if ($service.Status -ne 'Running')
{
    throw 'Start the BDS service failed.'
}

LoggerInfo -Message 'Start the BDS service successfully'