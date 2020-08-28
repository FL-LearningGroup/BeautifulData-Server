namespace BDS.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    public class AssemblyInformation
    {
        public static string LocationFolder
        {
            get
            {
                return Path.GetDirectoryName(new AssemblyInformation().GetType().Assembly.Location);
            }
        }
        public static string ExecutingFolder
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }
    }
}
