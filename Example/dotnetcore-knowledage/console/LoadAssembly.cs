using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Runtime.Loader;
using System.Runtime.CompilerServices;
using System.IO;
using System.Threading;

namespace BDS.DotNetCoreKnowledage
{
    class PipelineAssembly: IDisposable
    {
        //private PipelineAssemblyLoadContext _assemblyLoadContext;
        //private Assembly _assembly;
        private string _assemblyPath;
        public PipelineAssembly(string assemblyPath)
        {
            _assemblyPath = assemblyPath;

        }
        // It is important to mark this method as NoInlining, otherwise the JIT could decide
        // to inline it into the Main method. That could then prevent successful unloading
        // of the plugin because some of the MethodInfo / Type / Plugin.Interface / HostAssemblyLoadContext
        // instances may get lifetime extended beyond the point when the plugin is expected to be
        // unloaded
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void LoadAssembly(out WeakReference alcWeakRef)
        {
            // Create a weak reference to the AssemblyLoadContext that will allow us to detect
            // when the unload completes.
            PipelineAssemblyLoadContext _assemblyLoadContext = new PipelineAssemblyLoadContext(_assemblyPath);
            alcWeakRef = new WeakReference(_assemblyLoadContext);
            Assembly _assembly = _assemblyLoadContext.LoadFromAssemblyPath(_assemblyPath);
            _assemblyLoadContext.Unload();
        }

        public void UnloadAssembly()
        {
            //_assemblyLoadContext.Unload();
            //_assemblyLoadContext.Dispose();
        }
        public void Dispose()
        {

        }
    }

    class LoadAssembly
    {
        static string assemblyPath = @"D:\Lucas\git\BeautifulData-Server\example\dotnetcore-knowledage\PlugingAssembly\bin\Debug\netstandard2.0\PlugingAssembly.dll";
        //static string assemblyPath = @"D:\Lucas\git\BeautifulData-Server\example\pipeline\fuyang\BDS.Pipeline.FuYang.dll";
        static void CallLoadAssembly()
        {

            List<WeakReference> WeakReferences = new List<WeakReference>();
            WeakReference weak;
            PipelineAssembly pipelineAssembly = new PipelineAssembly(assemblyPath);
            pipelineAssembly.LoadAssembly(out weak);
            Console.WriteLine(weak.IsAlive);
            for (int i = 0; weak.IsAlive && (i < 10); i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            Console.WriteLine("Unloaded asssembly successfully: {0}", !weak.IsAlive);
        }
        static void Main_Stop()
        {
            Process.StartTag();
            CallLoadAssembly();
            File.Delete(assemblyPath);
            Process.EndTag();
        }
    }
}
