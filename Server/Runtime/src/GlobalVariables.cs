using BDS.Runtime.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;

namespace BDS.Runtime
{
    internal static class GlobalVariables
    {
        private static ServerConfig _serverConfig;
        public static string WorkFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string ResourcesFolder = Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar;
        public static void GetAssemblied(string context)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            //Make an array for the list of assemblies.
            Assembly[] assems = currentDomain.GetAssemblies();

            //List the assemblies in the current application domain.
            Logger.Debug(String.Format("{0} -- List of assemblies loaded in current appdomain:", context));
            var orderbyAssems =  assems.OrderBy(assem => assem.FullName);
            foreach (Assembly assem in orderbyAssems)
                Logger.Debug(assem.ToString());
            Logger.Debug(String.Format("{0} -- List of assemblies done-------------------------------", context));
        }

        public static string ConvertDateTimeToStringFormat { get => "yyyy/MM/dd HH:mm:ss"; private set { } }

        public static ServerConfig ServerConfig { get => _serverConfig = _serverConfig ?? new ServerConfig(); set => _serverConfig = value; }
    }
}
