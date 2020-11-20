---
titile: BDS.Runtime design document
date: 2020/11/20
author: LucasYao
---
## Summary
The BDS.Runtime drive The BDS.Framework to run pipeline.

## Design

### Feature
+ Load and Unload the reference pipeline assembly.
The Unload is a high risk feature base on the NetCore provide assembly [unloadability](https://docs.microsoft.com/en-us/dotnet/standard/assembly/unloadability) function. 
Think in Two ways:
  - Run GC.Collect everytime after unload assembly successfully that consume performance
  - The assembly was unloaded successfully but it can't be delete.  
As stated above, The BDS.Runtime not support dynamic unload assembly.
