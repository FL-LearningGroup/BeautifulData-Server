#define DEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LibGit2Sharp;
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
        private List<AssemblyConfig> _removeAssemblyConfigList = new List<AssemblyConfig>();

        public List<AssemblyConfig> AssemblyConfigList { get {return _assemblyConfigList; } }
        public List<AssemblyConfig> AddAssemblyConfigList { get { return _addAssemblyConfigList; } }
        public List<AssemblyConfig> RemoveAssemblyConfigList { get { return _removeAssemblyConfigList; } }
        public PipelineHostService()
        {
        }
        private void ServiceStart()
        {
            ConfigurationWatcher.watcher.Created += AddAssemblyPath;
            ConfigurationWatcher.watcher.Changed += AddAssemblyPath;
            ConfigurationWatcher.watcher.EnableRaisingEvents = true;
            //Init load assembly path
            foreach (AssemblyConfig assemblyConfig in Config.LoadAssemblyConfig(AssemblyInformation.ExecutingFolder + @"\config\AssemblyConfig.xml"))
            {
                if (assemblyConfig.AssemplyStatus.ToLower() == AssemblyConfigStatus.ADD.ToLower())
                {
                    _addAssemblyConfigList.Add(assemblyConfig);
                }
            }
        }
        private void AddAssemblyPath(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine("Trigger watcher file.");
            List<AssemblyConfig> assemblyConfigs = Config.LoadAssemblyConfig(e.FullPath);
            if (assemblyConfigs is null)
                return;
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
                    //Note: Change trigger twice. such as notepadd++. 
                    if (_addAssemblyConfigList.Contains(assemblyConfig))
                    {
                        continue;
                    }
                    _addAssemblyConfigList.Add(assemblyConfig);
                    continue;
                }
                //Remove assembly
                if (assemblyConfig.AssemplyStatus.ToLower() == AssemblyConfigStatus.REMOVE.ToLower())
                {
                    if (_assemblyConfigList.Contains(assemblyConfig))
                    {
                        //Note: Change trigger twice. such as notepadd++. 
                        if (_removeAssemblyConfigList.Contains(assemblyConfig))
                        {
                            continue;
                        }
                        _removeAssemblyConfigList.Add(assemblyConfig);
                        continue;
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
                _assemblyConfigList.Add(assemblyConfig);
            }
            _addAssemblyConfigList.Clear();
        }
        public void RemovePipeline()
        {
            if (_removeAssemblyConfigList.Count == 0)
                return;
            List<AssemblyConfig> removedAssemblyPathList = new List<AssemblyConfig>();
            foreach (AssemblyConfig assemblyConfig in _removeAssemblyConfigList)
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
                _removeAssemblyConfigList.Remove(assemblyConfig);
            }

        }
        private void ShowDebugInfo()
        {
            Console.WriteLine("Assembly Config^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
            Console.WriteLine("AssemblyConfigList--------------------");
            foreach(var config in _assemblyConfigList)
            {
                Console.WriteLine("{0} - {1} - {2}", config.AssemblyKey, config.AssemblyPath, config.AssemplyStatus);
            }
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("AddAssemblyConfigList-----------------");
            foreach (var config in _addAssemblyConfigList)
            {
                Console.WriteLine("{0} - {1} - {2}", config.AssemblyKey, config.AssemblyPath, config.AssemplyStatus);
            }
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("RemoveAssemblyConfigList--------------");
            foreach (var config in _removeAssemblyConfigList)
            {
                Console.WriteLine("{0} - {1} - {2}", config.AssemblyKey, config.AssemblyPath, config.AssemplyStatus);
            }
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Pipleine Collections^^^^^^^^^^^^^^^^^^");
            foreach(var pipeline in _pipelineCollections)
            {
                Console.WriteLine("{0} - {1}", pipeline.AssemblyKey, pipeline.AssemblyPath);
            }
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Pipeline Service start.");
            ServiceStart();
            while (!stoppingToken.IsCancellationRequested)
            {
                ShowDebugInfo();
                AddPipeline();
                RemovePipeline();
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}