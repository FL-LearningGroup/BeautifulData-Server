using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BDS.Runtime
{
    internal class PipelineFolderWatcher
    {
        private readonly static string _folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pipeline";

        public static FileSystemWatcher watcher;
        static PipelineFolderWatcher()
        {
            if (!Directory.Exists(_folderPath))
            {
                throw new Exception("pipeline folder file not found.");
            }
            watcher = new FileSystemWatcher();
            watcher.Path = _folderPath;
            watcher.IncludeSubdirectories = true;
            watcher.Filter = "BDS.Pipeline*.dll";
            watcher.NotifyFilter = NotifyFilters.LastWrite
                                    | NotifyFilters.LastAccess
                                    | NotifyFilters.FileName
                                    | NotifyFilters.DirectoryName;
        }
    }
}
