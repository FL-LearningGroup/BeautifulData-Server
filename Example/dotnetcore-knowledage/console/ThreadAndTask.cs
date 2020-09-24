using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
/*
 * Test: Delete file during the program running.
 * 1. One Thread: Delete file when FileStream closed.
 * 2. Task: When task complete and 
 * 
 */
namespace BDS.DotNetCoreKnowledage
{
    class ThreadAndTask
    {
        private static string _assemblyPath = @"D:\Lucas\git\BeautifulData-Server\example\dotnetcore-knowledage\PlugingAssembly\bin\Debug\netstandard2.0\PlugingAssembly.dll";
        public static void LoadAssembly()
        {
            var assembly = Assembly.LoadFrom(_assemblyPath);
        }
        static void Main_Stop()
        {
            Thread.CurrentThread.Name = "Main";
            Process.StartTag();
            LoadAssembly();
            Console.WriteLine("Hello from thread '{0}'.",Thread.CurrentThread.Name);
            File.Delete(_assemblyPath);
            Process.EndTag();
        }
    }
}
