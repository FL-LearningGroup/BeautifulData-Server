using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    public class DynamicLoadDLL
    {
        static void Example()
        {
            var DLL = Assembly.LoadFile(@"D:\Lucas\git\BeautifulData-Server\example\dotnetstandard.example.classlib\bin\Debug\netstandard2.0\dotnetstandard.example.classlib.dll");

            foreach (Type type in DLL.GetExportedTypes())
            {
                if (type.Name == "Class1")
                {
                    var c = Activator.CreateInstance(type);
                    type.InvokeMember("Output", BindingFlags.InvokeMethod, null, c, new object[] { @"Hello" });
                }
            }
        }
        static void Main_Stop()
        {
            Process.StartTag();
            var DLL = Assembly.LoadFrom(@"D:\Lucas\git\BeautifulData-Server\server\pipeline\fuyang\src\bin\Debug\netcoreapp3.1\BDS.Pipeline.FuYang.dll");
            foreach (Type type in DLL.GetExportedTypes())
            {
                if (type.Name.Contains("Pipeline_"))
                {
                    var pipeline = Activator.CreateInstance(type);
                    type.InvokeMember("StartWork", BindingFlags.InvokeMethod, null, pipeline, null);
                    //break;
                }
            }
            Process.EndTag();
        }
    }
}
