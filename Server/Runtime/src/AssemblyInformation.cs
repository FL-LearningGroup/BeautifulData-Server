namespace BDS.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Assembly information
    /// </summary>
    public class AssemblyInformation
    {
        /// <summary>
        /// Location path of assembly file.
        /// </summary>
        public static string LocationFolder
        {
            get
            {
                return Path.GetDirectoryName(new AssemblyInformation().GetType().Assembly.Location);
            }
        }
        /// <summary>
        /// Execute path of assembly file.
        /// </summary>
        public static string ExecutingFolder
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }
    }
}
