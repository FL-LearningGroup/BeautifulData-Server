using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BDS.Runtime
{
    public class PipelineWorker : BackgroundService, IPipelineWorker
    {
        private List<Pipeline> _pipelineCollections = new List<Pipeline>();
        private readonly string _assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pipeline";
        /// <summary>
        /// Store the loaded assembly path.
        /// </summary>
        private List<string> _assemblyPathList = new List<string>();
        /// <summary>
        /// Store the path of assembly waiting to be loaded.
        /// </summary>
        private List<string> _waitAssemblyPathList = new List<string>();

        public List<string> AssemblyPathList { get; }
        public List<string> WaitAssemblyPathList { get; }
        public PipelineWorker()
        {
            string[] filePathArray = Directory.GetFiles(_assemblyFolder, "BDS.Pipeline*.dll", SearchOption.AllDirectories);
            foreach(string filePath in filePathArray)
            {
                _waitAssemblyPathList.Add(filePath);
            }
        }

        public void LoadAssembly()
        {
            if (_waitAssemblyPathList.Count == 0)
                return;
           
            List<string> removeAssemblyPathList = new List<string>();
            foreach (string assemblyPath in _waitAssemblyPathList)
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                bool runFlag = false;
                foreach (Pipeline pipelineItem in _pipelineCollections)
                {
                    // Pipeline existed in pipeline collections.
                    if (pipelineItem.FullName == assembly.FullName)
                    {
                        //Pipeline running
                        if (pipelineItem.status == PipelineStatus.Running)
                        {
                            runFlag = true;
                            break;
                        }
                        _pipelineCollections.Remove(pipelineItem);
                        break;
                    }
                }
                if(!runFlag)
                {
                    Pipeline pipeline = new Pipeline(assembly, assembly.FullName, DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fffffff"));
                    _pipelineCollections.Add(pipeline);
                    _assemblyPathList.Add(assemblyPath);
                    removeAssemblyPathList.Add(assemblyPath);
                    // Remove element during foreach, cause System.InvalidOperationException: 
                    //'Collection was modified; enumeration operation may not execute.'
                    //_waitAssemblyPathList.Remove(assemblyPath);
                }
            }

            foreach(string assemblyPath in removeAssemblyPathList)
            {
                _waitAssemblyPathList.Remove(assemblyPath);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                /*
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
                */
                Console.WriteLine("Task execute.");
                foreach (var item in _assemblyPathList)
                {
                    Console.WriteLine("assemblyPath:{0}",item);
                }
                foreach (var item in _waitAssemblyPathList)
                {
                    Console.WriteLine("wait assemblyPath:{0}", item);
                }
                foreach (var item in _pipelineCollections)
                {
                    Console.WriteLine("Assembly:{0}, {1}", item.FullName, item.LoadDate);
                }
                LoadAssembly();
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
