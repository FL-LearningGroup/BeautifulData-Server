using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BDS.Runtime
{
    internal static class GlobalConstant
    {
        public static string WorkFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
