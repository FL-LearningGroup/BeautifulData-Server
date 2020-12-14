using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BDS.Runtime.Models;
using BDS.Runtime.Respository;
using BDS.Framework;
using log4net.Util;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Threading;
using BDS.Runtime.Respository.Models;

namespace BDS.Runtime
{
    /// <summary>
    /// Invoke pipeline
    /// </summary>
    /// <designspec>
    /// Load dll
    /// Execute pipleine
    /// Record the results of execution
    /// Make results of execution into history table
    /// </designspec>
    internal class DockPipelineOperations: DockPipeline, IDisposable
    {
        public DockPipelineOperations(string name, string dllPath)
        {
            Name = name;
            DllPath = dllPath;
            //Set pipeline can be invokeable
            workPipelines = new List<WorkPipeline>();
            Initialization();
        }
        
        private void Initialization()
        {
            StatusEvent += RecordResultsExecution;
            try
            {
                assemblyLoadContext = new PipelineAssemblyLoadContext(DllPath);
                LoadPipelineDT = DateTime.Now;
                pipelineAssembly = assemblyLoadContext.LoadFromAssemblyPath(DllPath);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("The pipeline {0} init failed in constructor method, error message: {1}", Name, ex.Message));
                ExecutionMessage.Append(String.Format("The pipeline init failed in constructor method, error message: {0}", ex.Message));
                Status = WorkPipelineStatus.Failed;
            }
        }

        private PipelineTaskResult ExecutePipelineAsync()
        {
            lock(this)
            {
                try
                {
                    // Load assembly
                    // Get all types of the assembly.
                    Type[] pipelineTypes = pipelineAssembly.GetTypes();
                    //Get pipline of inherit WorkPipeline class
                    foreach (Type pipelineType in pipelineTypes)
                    {              
                        if (pipelineType.BaseType.Name == typeof(WorkPipeline).Name)
                        {
                            object pipelineInstance = Activator.CreateInstance(pipelineType);
                            workPipelines.Add((WorkPipeline)pipelineInstance);
                        }
                    }
                    // Execute pipeline
                    Status = WorkPipelineStatus.Running;
                    ExecuteStartDT = DateTime.Now;
                    foreach(WorkPipeline workPipeline in workPipelines)
                    {
                       workPipeline.Processor();
                    }
                    ExecuteEndDT = DateTime.Now;
                    foreach(WorkPipeline pipeline in workPipelines)
                    {
                        if (pipeline.Status == WorkPipelineStatus.Failed)
                        {
                            Status = WorkPipelineStatus.Failed;
                            break;
                        }
                    }
                    if (Status != WorkPipelineStatus.Failed) 
                        Status = WorkPipelineStatus.Success;
                    //Set pipeline can be invokeable
                }
                catch (Exception ex)
                {
                    Logger.Error(String.Format("The pipeline {0} executed failed. detail error message: {1}", Name, ex.Message));
                    ExecutionMessage.Append(String.Format("The pipeline {0} executed failed. detail error message: {1}", Name, ex.Message));
                    Status = WorkPipelineStatus.Failed;
                }
                finally
                {
                    Status = WorkPipelineStatus.Wait;
                }
                return new PipelineTaskResult() {Name = this.Name, Status = this.Status };
            }
        }
        private void RecordResultsExecution(object source, PipelineStatusEventArgs args)
        {
            // Save executed info to database
            using (var context = new MySqlContext())
            {
                DockPipeline pipeline = context.DockPipeline.Find(this.Name);
                // First insert
                if (pipeline is null)
                {
                    pipeline = this;
                    context.DockPipeline.Add(pipeline);
                }
                else if (args.Status == WorkPipelineStatus.Failed)
                {
                    pipeline.ExecutionMessage = this.ExecutionMessage;
                }
                else
                {
                    // Update row
                    pipeline.Status = this.Status;
                    pipeline.ExecuteStartDT = this.ExecuteStartDT;
                    pipeline.ExecuteEndDT = this.ExecuteEndDT;
                    pipeline.LoadPipelineDT = this.LoadPipelineDT;

                }
                DockPipelineHistory dockPipelineHistory = new DockPipelineHistory();
                SaveToHistoryTable(pipeline, dockPipelineHistory);
                context.DockPipelineHistory.Add(dockPipelineHistory);
                context.SaveChanges();
            }
        }
        private void SaveToHistoryTable(DockPipeline dockPipeline, DockPipelineHistory dockPipelineHistory)
        {
            dockPipelineHistory.Name = dockPipeline.Name;
            dockPipelineHistory.Status = dockPipeline.Status;
            dockPipelineHistory.ExecuteStartDT = dockPipeline.ExecuteStartDT;
            dockPipelineHistory.ExecuteEndDT = dockPipeline.ExecuteEndDT;
            dockPipelineHistory.LoadPipelineDT = dockPipeline.LoadPipelineDT;
            dockPipelineHistory.ExecutionMessage = dockPipeline.ExecutionMessage;
        }
        public override async Task<PipelineTaskResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => ExecutePipelineAsync(), cancellationToken);
        }

        public void Dispose()
        {
            assemblyLoadContext.Unload();
            GC.Collect();
        }
    }
}
