$params = @{
    Name = "BDS"
    BinaryPathName = "C:\Program Files\BDS\BDS.Runtime\BDS.Runtime.ex"
    DependsOn = "NetLogon"
    DisplayName = "Test Service"
    StartupType = "Manual"
    Description = "Get data form web world."
  }

  New-Service @params