﻿using System;
using System.IO;
using System.Reflection;
using System.Security.Permissions;

namespace BDS.DotNetCoreKnowledage
{


    public class WatcherFile
    {
        public static void Main_Stop()
        {
            Run();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void Run()
        {
            //string[] args = Environment.GetCommandLineArgs();

            // If a directory is not specified, exit program.
            /*
            if (args.Length != 2)
            {
                // Display the proper way to call the program.
                Console.WriteLine("Usage: Watcher.exe (directory)");
                return;
            }
            */
            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = @"D:\Lucas\git\BeautifulData-Server\server\runtime\src\bin\Debug\netcoreapp3.1\config";
                //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\tmp"; //
                watcher.IncludeSubdirectories = true;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = "*";

                // Add event handlers.
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                //watcher.Deleted += OnChanged;
                //watcher.Renamed += OnRenamed;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e) =>
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
        //Result:
        //File: D:\Lucas\git\BeautifulData-Server\example\dotnetcore-knowledage\console\bin\Debug\netcoreapp3.1\tmp\TextFile1.txt Changed
        //Changed
        private static void OnRenamed(object source, RenamedEventArgs e) =>
            // Specify what is done when a file is renamed.
            Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");
    }
}

