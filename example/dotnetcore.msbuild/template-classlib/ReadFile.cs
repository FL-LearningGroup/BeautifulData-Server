using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetCore.MSBuild.ClassLib
{
    public class ReadFile
    {
        public void read()
        {
            if (!File.Exists(AssemblyInformation.LocationFolder + @"\config\TextFile1.txt"))
            {
                throw new NullReferenceException("TextFile1.txt config file not found.");
            }
           
        }
    }
}
