using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Security.Permissions;

namespace BDS.Runtime
{
    /// <summary>
    /// Watch config folder.
    /// </summary>
    internal class ConfigurationWatcher
    {
        private readonly static string _folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\config";

        public static FileSystemWatcher watcher;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        static ConfigurationWatcher()
        {
            if (!Directory.Exists(_folderPath))
            {
                throw new Exception("config folder file not found.");
            }
            watcher = new FileSystemWatcher();
            watcher.Path = _folderPath;
            watcher.IncludeSubdirectories = true;
            watcher.Filter = "*";
            watcher.NotifyFilter = NotifyFilters.LastWrite
                                    | NotifyFilters.FileName
                                    | NotifyFilters.DirectoryName;
        }
    }
}
