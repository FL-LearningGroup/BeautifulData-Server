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
        /// <summary>
        /// The path of assembly need be remove.
        /// </summary>
        private List<string> _removeAssemblyPathList = new List<string>();

        public List<string> AssemblyPathList { get {return _assemblyPathList; } }
        public List<string> WaitAssemblyPathList { get { return _waitAssemblyPathList; } }
        public List<string> RemoveAssemblyPathList { get { return _removeAssemblyPathList; } }
        public PipelineWorker()
        {
            string[] filePathArray = Directory.GetFiles(_assemblyFolder, "BDS.Pipeline*.dll", SearchOption.AllDirectories);
            foreach(string filePath in filePathArray)
            {
                _waitAssemblyPathList.Add(filePath);
            }
            PipelineFolderWatcher.watcher.Created += AddAssemblyPath;
            PipelineFolderWatcher.watcher.Deleted += RemoveAssemblyPath;
            //PipelineFolderWatcher.watcher.Changed += AddAssemblyPath;
        }

        private void AddAssemblyPath(object source, FileSystemEventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" +e.FullPath;
            _waitAssemblyPathList.Add(path);
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
        }
        private void RemoveAssemblyPath(object source, FileSystemEventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + e.FullPath;
            _removeAssemblyPathList.Add(path);
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
        }
        public void LoadAssembly()
        {
            if (_waitAssemblyPathList.Count == 0)
                return;
           
            List<string> addedAssemblyPathList = new List<string>();
            foreach (string assemblyPath in _waitAssemblyPathList)
            {
                Assembly assembly;
                //Loaded assembly by bytes, The assembly file can be delete.
                //Warning: Application not unload assembly, whether cause memory continue growth 
                assembly = Assembly.Load(File.ReadAllBytes(assemblyPath));
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
                //Handle PipelineCollections is null
                if (!runFlag)
                {
                    Pipeline pipeline = new Pipeline(assembly, assemblyPath, assembly.FullName, DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fffffff"));
                    _pipelineCollections.Add(pipeline);
                    _assemblyPathList.Add(assemblyPath);
                    addedAssemblyPathList.Add(assemblyPath);
                    // Remove element during foreach, cause System.InvalidOperationException: 
                    //'Collection was modified; enumeration operation may not execute.'
                    //_waitAssemblyPathList.Remove(assemblyPath);
                }
            }

            foreach(string assemblyPath in addedAssemblyPathList)
            {
                _waitAssemblyPathList.Remove(assemblyPath);
            }
        }

        public void UnloadAssembly()
        {
            if (_removeAssemblyPathList.Count == 0)
                return;
            List<string> removedAssemblyPathList = new List<string>();
            foreach (string assemblyPath in _removeAssemblyPathList)
            {
                foreach (Pipeline pipelineItem in _pipelineCollections)
                {
                    // Pipeline existed in pipeline collections.
                    if (pipelineItem.AssemblyPath == assemblyPath)
                    {
                        //Pipeline running
                        if (pipelineItem.status == PipelineStatus.Running)
                        {
                            break;
                        }
                        _pipelineCollections.Remove(pipelineItem);
                        _assemblyPathList.Remove(assemblyPath);
                        removedAssemblyPathList.Add(assemblyPath);
                        break;
                    }
                }
            }

            foreach (string assemblyPath in removedAssemblyPathList)
            {
                _removeAssemblyPathList.Remove(assemblyPath);
            }

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Pipeline Service start.");
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
                    Console.WriteLine("Assembly:{0}, {1}, {2}", item.FullName, item.LoadDate, item.AssemblyPath);
                }
                Console.WriteLine("----------------------------------------");
                LoadAssembly();
                UnloadAssembly();
                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
