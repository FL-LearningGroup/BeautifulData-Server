using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BDS.Runtime.Models;
using BDS.Runtime.DataBase;
using BDS.Framework;
using log4net.Util;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace BDS.Runtime
{
    /// <summary>
    /// Invoke pipeline
    /// </summary>
    /// <designspec>
    /// 1. Load dll
    /// 2. Execute pipleine
    /// 3. Set schedule time
    /// 4. Record the results of execution
    /// 5. Make results of execution into history table
    /// </designspec>
    internal class DockPipelineOperations: DockPipeline, IDisposable
    {
        public DockPipelineOperations(PipelineAssemblyConfig assemblyConfigParam)
        {
            Name = assemblyConfigParam.AssemblyKey;
            //Set pipeline can be invokeable
            assemblyConfig = assemblyConfigParam;
            workPipelines = new List<WorkPipeline>();

            Initialization();
        }
        
        private void Initialization()
        {
            StatusEvent += RecordResultsExecution;
            try
            {
                DateTime _lastExecuteTime = LastExecuteDT;
                DateTime _nextExecuteTime = NextExecuteDT;
                PiplelineScheduleTime.SetScheduleTime(assemblyConfig.ScheduleTime, out scheduleTime, out _lastExecuteTime, ref _nextExecuteTime);
                LastExecuteDT = _lastExecuteTime;
                NextExecuteDT = _nextExecuteTime;

                assemblyLoadContext = new PipelineAssemblyLoadContext(assemblyConfig.AssemblyPath);
                LoadPipelineDT = DateTime.Now;
                pipelineAssembly = assemblyLoadContext.LoadFromAssemblyPath(assemblyConfig.AssemblyPath);

                InvokeStatus = PipelineInvokeStatus.Invokeable;
                Status = WorkPipelineStatus.Wait;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("The pipeline {0} init failed in constructor method, error message: {1}", Name, ex.Message));
                ExecutionMessage.Append(String.Format("The pipeline init failed in constructor method, error message: {0}", ex.Message));
                InvokeStatus = PipelineInvokeStatus.InvokeUnable;
                Status = WorkPipelineStatus.Failed;
            }
        }

        private async Task ExecutePipelineAsync()
        {
            object lockobject = new object();
            lock(lockobject)
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
                    //Set next execute time.
                    DateTime _lastExecuteTime = LastExecuteDT;
                    DateTime _nextExecuteTime = NextExecuteDT;
                    PiplelineScheduleTime.SetNextExecuteTime(scheduleTime, out _lastExecuteTime, ref _nextExecuteTime);
                    LastExecuteDT = _lastExecuteTime;
                    NextExecuteDT = _nextExecuteTime;
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
                    InvokeStatus = PipelineInvokeStatus.Invokeable;
                    Status = WorkPipelineStatus.Wait; //Cause exception
                }
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
                    pipeline.LastExecuteDT = this.LastExecuteDT;
                    pipeline.NextExecuteDT = this.NextExecuteDT;
                    pipeline.ExecuteStartDT = this.ExecuteStartDT;
                    pipeline.ExecuteEndDT = this.ExecuteEndDT;
                    pipeline.LoadPipelineDT = this.LoadPipelineDT;
                    pipeline.InvokeStatus = this.InvokeStatus;

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
            dockPipelineHistory.InvokeStatus = dockPipeline.InvokeStatus;
            dockPipelineHistory.LastExecuteDT = dockPipeline.LastExecuteDT;
            dockPipelineHistory.NextExecuteDT = dockPipeline.NextExecuteDT;
            dockPipelineHistory.ExecuteStartDT = dockPipeline.ExecuteStartDT;
            dockPipelineHistory.ExecuteEndDT = dockPipeline.ExecuteEndDT;
            dockPipelineHistory.LoadPipelineDT = dockPipeline.LoadPipelineDT;
            dockPipelineHistory.ExecutionMessage = dockPipeline.ExecutionMessage;
        }
        public override async Task ExecuteAsync()
        {
            await ExecutePipelineAsync();
        }

        public void Dispose()
        {
            assemblyLoadContext.Unload();
            GC.Collect();
        }
    }
}
