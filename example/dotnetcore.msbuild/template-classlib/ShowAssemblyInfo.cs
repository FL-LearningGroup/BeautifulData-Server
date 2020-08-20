using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DotNetCore.MSBuild.ClassLib
{
    public class ShowAssemblyInfo
    {
        public string Location { get; set; }
        public string ExecuteFolder { get; set; }
        public ShowAssemblyInfo()
        {
            AssemblyInfo assemblyInfo = new AssemblyInfo();
            Location = assemblyInfo.Location();
            ExecuteFolder = AssemblyInfo.ExecutingFolder();
        }
    }
}
