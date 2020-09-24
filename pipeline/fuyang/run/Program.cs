using System;
using System.Collections.Generic;
using System.Reflection;
//using BDS.Pipeline.FuYang;

namespace BDS.Pipeline.FuYang.Run
{
    class Program
    {
        readonly static string dllPath = @"D:\Lucas\git\BeautifulData-Server\server\pipeline\fuyang\src\bin\Debug\netcoreapp3.1\BDS.Pipeline.FuYang.dll";
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
        static bool DynamicLoadDLL()
        {
            Assembly dll = Assembly.LoadFrom(dllPath);
            foreach (Type type in dll.GetExportedTypes())
            {
                if (type.Name.Contains("AssemblyInformation"))
                {
                    var assembly = Activator.CreateInstance(type);
                    PropertyInfo locationFolder = type.GetProperty("LocationFolder");
                    PropertyInfo executingFolder = type.GetProperty("ExecutingFolder");
                    Console.WriteLine("location folder: {0}", locationFolder.GetValue(assembly));
                    Console.WriteLine("executing folder: {0}", executingFolder.GetValue(assembly));
                }
                if (type.Name.Contains("Pipeline_"))
                {
                    var pipelineInstance = Activator.CreateInstance(type);
                    var result = type.InvokeMember("StartWork", BindingFlags.InvokeMethod, null, pipelineInstance, null);
                    return Convert.ToBoolean(result);
                }
                return false;
            }
            return false;
        }
        static bool InvokeStartWork()
        {
            return Pipeline_FuYangNews.StartWork();
        }
        static void Main(string[] args)
        {
            StartTag();
            bool result = InvokeStartWork();
            Console.WriteLine("Pipeline execute: {0}", result);
            EndTag();
        }
    }
}
