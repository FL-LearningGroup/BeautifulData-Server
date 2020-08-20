using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DotNetCore.MSBuild.TemplateConsole
{
    public class AssemblyLocation
    {
        /// <summary>
        /// Get DLL current path by object.
        /// </summary>
        /// <returns>DLL current path</returns>
        public String GetAssemblyLocation()
        {
            return this.GetType().Assembly.Location;
        }
        /// <summary>
        /// Static get dLL current folder
        /// </summary>
        /// <returns>DLL current folder</returns>
        public static string ExecutingFolder()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
