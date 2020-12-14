using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;

namespace BDS.Runtime
{
    internal class ServerConfigWatcher
    {
        private readonly static string _folderPath = GlobalVariables.WorkFolder + Path.DirectorySeparatorChar + "Config";

        public static FileSystemWatcher watcher;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        static ServerConfigWatcher()
        {
            if (!Directory.Exists(_folderPath))
            {
                Logger.Fatal("The config folder file not found.");
                throw new Exception("The config folder file not found.");
            }
            watcher = new FileSystemWatcher();
            watcher.Path = _folderPath;
            watcher.IncludeSubdirectories = false;
            watcher.Filter = "ServerConfig.xml";
            //Raised FileSystemWatcher changed twice event while use notepadd++.
            watcher.NotifyFilter = NotifyFilters.LastWrite;
        }
    }
}
