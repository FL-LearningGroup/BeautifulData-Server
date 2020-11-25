using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BDS.Runtime.Models;
using BDS.Runtime.DataBase;
using System.IO;
using System.Linq;
using log4net.Util;
using BDS.Framework;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BDS.Runtime
{
    /// <summary>
    /// Function:
    /// 1. Monitor assembly confg.
    /// 2. New pipeline
    /// </summary>
    internal class PipelineHostService : BackgroundService, IPielineHostService
    {
        private List<PipelineAssemblyConfig> _addPipelineAssemblies;
        private List<PipelineAssemblyConfig> _removePipelineAssemblies;
        private List<Pipeline> _pipelines;
        public List<PipelineAssemblyConfig> AddPipelineAssemblies { get { return _addPipelineAssemblies; } }
        public List<PipelineAssemblyConfig> RemovePipelineAssemblies { get { return _removePipelineAssemblies; } }
        public List<Pipeline> Pipelines { get { return _pipelines; } }
        
        public PipelineHostService()
        {
            _addPipelineAssemblies = new List<PipelineAssemblyConfig>();
            _removePipelineAssemblies = new List<PipelineAssemblyConfig>();
            _pipelines = new List<Pipeline>();
        }

        private void InitialServer()
        {
            ServerConfigWatcher.watcher.Created += AddOrRemovePipelineAssembly;
            ServerConfigWatcher.watcher.Changed += AddOrRemovePipelineAssembly;
            ServerConfigWatcher.watcher.EnableRaisingEvents = true;
            //Load pipelne assemblies from the configuration file at the initial time.
            foreach (PipelineAssemblyConfig assemblyConfig in ServerConfig.LoadAssemblyConfig(GlobalConstant.WorkFolder + Path.DirectorySeparatorChar +"Config" + Path.DirectorySeparatorChar + "AssemblyConfig.xml"))
            {
                if (assemblyConfig.AssemblyStatus.ToLower() == PipelineAssemblyStatus.ADD.ToLower())
                {
                    _addPipelineAssemblies.Add(assemblyConfig);
                }
            }
            
            CheckOrCreateDBSchema();
        }

        private void CheckOrCreateDBSchema()
        {
            using (var context = new MySqlContext())
            {
                try
                {
                    bool isDataBase = context.Database.EnsureCreated();
                    if (isDataBase)
                    {
                        Logger.Info("Created database schema.");
                    }
                    else
                    {
                        Logger.Info("The database schema already existed.");
                    }
                }
                catch(Exception ex)
                {
                    Logger.Fatal(String.Format("Ensure created database failed. exception message: {0}", ex.Message));
                    throw new Exception(String.Format("Ensure created database failed. exception message: {0}", ex.Message));

                }
            }
        }

        private void DisplayPipelineAssemblies()
        {
            Logger.Debug("------------------Pipeline Assemblies------------------------------");
            Logger.Debug("------------------Add Pipeline Assemblies--------------------------");
            _addPipelineAssemblies.ToList().ForEach(item => Logger.Info(String.Format("Add: AssemlyKey: {0}, AssemblyStatus: {1}", item.AssemblyKey, item.AssemblyStatus)));
            Logger.Debug("------------------Remove Pipeline Assemblies-----------------------");
            _removePipelineAssemblies.ToList().ForEach(item => Logger.Info(String.Format("Remove: AssemlyKey: {0}, AssemblyStatus: {1}", item.AssemblyKey, item.AssemblyStatus)));
            Logger.Info("------------------Pipeline Assemblies------------------------------");
        }

        private void DisplayPipelines()
        {
            Logger.Debug("------------------Pipeline------------------------------");
            _pipelines.ForEach(item => Logger.Info(String.Format("Pipeline: AssemlyKey: {0}", item.Name)));
            Logger.Debug("------------------Pipeline------------------------------");
        }
        private void AddOrRemovePipelineAssembly(object source, FileSystemEventArgs eventArgs)
        {
            List<PipelineAssemblyConfig> pipelineAssemblies = null;
            int checkAssemblyFileLock = 0; //The assembly config file is open already with the another application.
            while((pipelineAssemblies is null) && (checkAssemblyFileLock < 3))
            {
                pipelineAssemblies = ServerConfig.LoadAssemblyConfig(eventArgs.FullPath);
                checkAssemblyFileLock++;
            }
            if (pipelineAssemblies is null) return; // Open assembly configuration file failed.
            bool isPipelineExisted = default(Boolean);
            foreach (PipelineAssemblyConfig pipelineAssembly in pipelineAssemblies)
            {
                //Check if it already exists in the pipeline
                foreach (var pipeline in _pipelines)
                {
                    if (pipeline.Name == pipelineAssembly.AssemblyKey)
                    {
                        isPipelineExisted = true;
                    }
                }
                //Add pipeline assembly in the pipeline.
                if (pipelineAssembly.AssemblyStatus.ToLower() == PipelineAssemblyStatus.ADD.ToLower())
                {
                    // Already exists in the pipeline.
                    if (isPipelineExisted)
                    {
                        continue;
                    }
                    // Already exists in the add pipeline assemblies.
                    if (_addPipelineAssemblies.Contains(pipelineAssembly))
                    {
                        continue;
                    }
                    // Add pipline assembly in the add pipeline assemblies.
                    _addPipelineAssemblies.Add(pipelineAssembly);
                    continue;
                }
                //Remove pipeline assembly in the remove pipeline assemblies.
                if (pipelineAssembly.AssemblyStatus.ToLower() == PipelineAssemblyStatus.REMOVE.ToLower())
                {
                    // Already exsits in the pipeline
                    if (isPipelineExisted)
                    {
                        // Alread exsits in the remove pipeline assemblies. 
                        if (_removePipelineAssemblies.Contains(pipelineAssembly))
                        {
                            continue;
                        }
                        // Add pipeline asemblyies in the remove pipeline assemblies.
                        _removePipelineAssemblies.Add(pipelineAssembly);
                        continue;
                    }
                }
            }
            DisplayPipelineAssemblies();
        }
        private void AddOrRemovePipeline()
        {
            if (_addPipelineAssemblies.Count == 0 && _removePipelineAssemblies.Count == 0) return;
            // Add new pipeline in the pipelines
            foreach (PipelineAssemblyConfig assemblyConfig in _addPipelineAssemblies)
            {
                _pipelines.Add(new DockPipelineOperations(assemblyConfig));
            }
            // Clear the add pipeline assemblies
            _addPipelineAssemblies.Clear();
            
            // Remove pipeline in the pipelines.
            List<PipelineAssemblyConfig> suceessRemoveAssemblies = new List<PipelineAssemblyConfig>();
            foreach (PipelineAssemblyConfig pipelineAssembly in _removePipelineAssemblies)
            {
                Pipeline pipeline = _pipelines.Find(item => item.Name == pipelineAssembly.AssemblyKey);
                if (pipeline != null)
                {
                    //Check pipeline status
                    if (pipeline.Status != WorkPipelineStatus.Running)
                    {
                        pipeline.UnloadPipelineDT = DateTime.Now;
                        _pipelines.Remove(pipeline);
                        suceessRemoveAssemblies.Add(pipelineAssembly);
                    }
                }
            }
            if (suceessRemoveAssemblies.Count != 0)
            {
                // Remove already removed pipeline in the remove pipeline assemblies.
                suceessRemoveAssemblies.ForEach(item => _removePipelineAssemblies.Remove(item));
            }
            DisplayPipelines();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.Info("Start the BDS Pipeline Service");
            // Configure server
            InitialServer();
            while (!stoppingToken.IsCancellationRequested)
            {
                //Add or remove the pipeline from the pipeline collection.
                AddOrRemovePipeline();
                foreach(Pipeline pipeline in _pipelines)
                {
                    if (pipeline.InvokeStatus == PipelineInvokeStatus.Invokeable && DateTime.Now >= pipeline.NextExecuteDT)
                    {
                        //Set pipeline invoke unable that wait for pipeline execute complete.
                        pipeline.InvokeStatus = PipelineInvokeStatus.InvokeUnable;
                        Logger.Info(String.Format("Execute pipeline {0} ", pipeline.Name));
                        pipeline.ExecuteAsync();
                    }
                }
            }
        }
    }
}
