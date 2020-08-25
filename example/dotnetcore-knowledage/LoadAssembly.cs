using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Runtime.Loader;
using System.Runtime.CompilerServices;
using System.IO;

namespace BDS.DotNetCoreKnowledage
{
    class PipelineAssembly
    {
        private PipelineAssemblyLoadContext _assemblyLoadContext;
        private Assembly _assembly;
        private string _assemblyPath;
        public PipelineAssembly(string assemblyPath)
        {
            _assemblyLoadContext = new PipelineAssemblyLoadContext(assemblyPath);
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
            alcWeakRef = new WeakReference(_assemblyLoadContext);
            _assembly = _assemblyLoadContext.LoadFromAssemblyPath(_assemblyPath);
        }

        public void UnloadAssembly()
        {
            _assemblyLoadContext.Unload();
        }
    }
    class LoadAssembly
    {
        static string path = @"D:\Lucas\git\BeautifulData-Server\example\pipeline\fuyang\BDS.Pipeline.FuYang.dll";
        static void Main()
        {
            Process.StartTag();
            /*
            List<WeakReference> WeakReferences = new List<WeakReference>();
            WeakReference weak;
            PipelineAssembly pipelineAssembly = new PipelineAssembly(@"D:\Lucas\git\BeautifulData-Server\example\pipeline\fuyang\BDS.Pipeline.FuYang.dll");
            pipelineAssembly.LoadAssembly(out weak);
            WeakReferences.Add(weak);
            pipelineAssembly.UnloadAssembly();
            */
            Assembly assembly = Assembly.Load(File.ReadAllBytes(path));
            //Assembly.
            Process.EndTag();
        }
    }
}
