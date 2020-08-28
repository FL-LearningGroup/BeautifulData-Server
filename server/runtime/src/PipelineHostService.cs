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
    internal class PipelineHostService : BackgroundService, IPipelineWorker
    {
        private List<Pipeline> _pipelineCollections = new List<Pipeline>();
        private readonly string _assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pipeline";
        /// <summary>
        /// Store the loaded assembly path.
        /// </summary>
        private List<AssemblyConfig> _assemblyConfigList = new List<AssemblyConfig>();
        /// <summary>
        /// Store the path of assembly waiting to be loaded.
        /// </summary>
        private List<AssemblyConfig> _addAssemblyConfigList = new List<AssemblyConfig>();
        /// <summary>
        /// The path of assembly need be remove.
        /// </summary>
        private List<AssemblyConfig> removeAssemblyConfigList = new List<AssemblyConfig>();

        public List<AssemblyConfig> AssemblyConfigList { get {return _assemblyConfigList; } }
        public List<AssemblyConfig> AddAssemblyConfigList { get { return _addAssemblyConfigList; } }
        public List<AssemblyConfig> RemoveAssemblyConfigList { get { return removeAssemblyConfigList; } }
        public PipelineHostService()
        {
        }
        private void ServiceStart()
        {
            ConfigurationWatcher.watcher.Created += AddAssemblyPath;
            ConfigurationWatcher.watcher.Changed += AddAssemblyPath;
            ConfigurationWatcher.watcher.EnableRaisingEvents = true;
        }
        private void AddAssemblyPath(object source, FileSystemEventArgs e)
        {
            List<AssemblyConfig> assemblyConfigs = Config.LoadAssemblyConfig(e.FullPath);
            foreach (AssemblyConfig assemblyConfig in assemblyConfigs)
            {
                //Add assembly
                if (assemblyConfig.AssemplyStatus.ToLower() == AssemblyConfigStatus.ADD.ToLower())
                {
                    //assembly exist
                    if (_assemblyConfigList.Contains(assemblyConfig))
                    {
                        continue;
                    }
                    _addAssemblyConfigList.Add(assemblyConfig);
                }
                //Remove assembly
                if (assemblyConfig.AssemplyStatus.ToLower() == AssemblyConfigStatus.REMOVE.ToLower())
                {
                    if (_assemblyConfigList.Contains(assemblyConfig))
                    {
                        removeAssemblyConfigList.Add(assemblyConfig);
                    }
                }

            }
        }
        public void AddPipeline()
        {
            if (_addAssemblyConfigList.Count == 0)
                return;
            foreach (AssemblyConfig assemblyConfig in _addAssemblyConfigList)
            {   
                _pipelineCollections.Add(new Pipeline(assemblyConfig.AssemblyKey, AssemblyInformation.ExecutingFolder + assemblyConfig.AssemblyPath));
            }
            _addAssemblyConfigList.Clear();
        }
        public void RemovePipeline()
        {
            if (removeAssemblyConfigList.Count == 0)
                return;
            List<AssemblyConfig> removedAssemblyPathList = new List<AssemblyConfig>();
            foreach (AssemblyConfig assemblyConfig in removeAssemblyConfigList)
            {
                foreach (Pipeline pipelineItem in _pipelineCollections)
                {
                    // Pipeline existed in pipeline collections.
                    if (pipelineItem.AssemblyKey== assemblyConfig.AssemblyKey)
                    {
                        //Pipeline running
                        if (pipelineItem.Status == PipelineStatus.Running)
                        {
                            break;
                        }
                        _pipelineCollections.Remove(pipelineItem);
                        _assemblyConfigList.Remove(assemblyConfig);
                        removedAssemblyPathList.Add(assemblyConfig);
                        break;
                    }
                }
            }

            foreach (AssemblyConfig assemblyConfig in removedAssemblyPathList)
            {
                removeAssemblyConfigList.Remove(assemblyConfig);
            }

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Pipeline Service start.");
            ServiceStart();
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
                Console.WriteLine("----------------------------------------");
                /*
                foreach (var item in _assemblyConfigList)
                {
                    Console.WriteLine("assemblyPath:{0}",item);
                }
                foreach (var item in _addAssemblyConfigList)
                {
                    Console.WriteLine("wait assemblyPath:{0}", item);
                }
                foreach (var item in _pipelineCollections)
                {
                    Console.WriteLine("Assembly:{0}, {1}, {2}", item.FullName, item.LoadDate, item.AssemblyPath);
                }
                Console.WriteLine("----------------------------------------");
                */
                //LoadAssembly();
                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
