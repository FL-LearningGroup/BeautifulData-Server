using System;
using System.IO;
using System.Reflection;
//using DotNetCore.MSBuild.ClassLib;

namespace DotNetCore.MSBuild.TemplateConsole
{
    class Program
    {
        static void StartTag()
        {
            Console.WriteLine("Process start up.");
            Console.WriteLine("----------------Process Step--------------------");
        }
        static void EndTag()
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Process End.");
            Console.ReadKey();
        }
        static void DynamicLoadDLl()
        {
            var DLL = Assembly.LoadFrom(@"D:\Lucas\git\BeautifulData-Server\example\dotnetcore.msbuild\template-classlib\bin\Debug\netcoreapp3.1\DotNetCore.MSBuild.ClassLib.dll");
            foreach (Type type in DLL.GetExportedTypes())
            {
                if (type.Name.Contains("AssemblyInformation"))
                {
                    var assembly = Activator.CreateInstance(type);
                    //var location = type.InvokeMember("GetAssemblyLocation", BindingFlags.InvokeMethod, null, assembly, null);
                    //var execute = type.InvokeMember("ExecutingFolder", BindingFlags.InvokeMethod, null, assembly, null);
                    PropertyInfo locationFolder = type.GetProperty("LocationFolder");
                    PropertyInfo executingFolder = type.GetProperty("ExecutingFolder");
                    Console.WriteLine("location folder: {0}", locationFolder.GetValue(assembly));
                    Console.WriteLine("executing folder: {0}", executingFolder.GetValue(assembly));
                }

                if (type.Name.Contains("ReadFile"))
                {
                    var readFile = Activator.CreateInstance(type);
                    type.InvokeMember("read", BindingFlags.InvokeMethod, null, readFile, null);
                }
            }
        }
        
        static void ManualLoadDLL()
        {
            /*
            ShowAssemblyInfo showAssemblyInfo = new ShowAssemblyInfo();
            Console.WriteLine("location: {0}", showAssemblyInfo.Location);
            Console.WriteLine("execute: {0}", showAssemblyInfo.ExecuteFolder);
            */
        }
        static void Main(string[] args)
        {
            StartTag();
            DynamicLoadDLl();
            EndTag();
        }
    }
}
