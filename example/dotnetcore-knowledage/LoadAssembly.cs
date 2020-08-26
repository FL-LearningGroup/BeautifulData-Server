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

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void LoadAssemblyOthersThread(string assemblyPath)
        {
            // Create a weak reference to the AssemblyLoadContext that will allow us to detect
            // when the unload completes.
            Console.WriteLine("The thread load assembly");
            PipelineAssemblyLoadContext _assemblyLoadContext = new PipelineAssemblyLoadContext(assemblyPath);
            Assembly _assembly = _assemblyLoadContext.LoadFromAssemblyPath(assemblyPath);
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
        static string path = @"D:\Lucas\git\BeautifulData-Server\example\pipeline\fuyang\BDS.Pipeline.FuYang.dll";
        static void CallLoadAssembly()
        {

            List<WeakReference> WeakReferences = new List<WeakReference>();
            WeakReference weak;
            PipelineAssembly pipelineAssembly = new PipelineAssembly(@"D:\Lucas\git\BeautifulData-Server\example\pipeline\fuyang\BDS.Pipeline.FuYang.dll");
            pipelineAssembly.LoadAssembly(out weak);
            //WeakReferences.Add(weak);
            //Console.WriteLine(weak.IsAlive);
            //pipelineAssembly.UnloadAssembly();
            Console.WriteLine(weak.IsAlive);
            pipelineAssembly = null;
            for (int i = 0; weak.IsAlive && (i < 10); i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            Console.WriteLine(weak.IsAlive);
        }
        static void Main()
        {
            Process.StartTag();
            Thread thread = new Thread(() => PipelineAssembly.LoadAssemblyOthersThread(path));
            thread.Start();
            while(true)
            {
                Console.WriteLine();
                char key = Console.ReadKey().KeyChar;
                if (key == 'q')
                {
                    break;
                }
                Console.WriteLine("Thread Status:{0}, {1}, {2}", thread.Name, thread.ThreadState, thread.IsAlive);
            }
            //thread.Abort();
            Process.EndTag();
        }
    }
}
