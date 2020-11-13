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
    /// </designspec>
    internal class DockPipelineBuilder: DockPipeline
    {
        public DockPipelineBuilder(PipelineAssemblyConfig assemblyConfig)
        {
            StatusEvent += RecordResultsExecution;
            Name = assemblyConfig.AssemblyKey;
            //Set pipeline can be invokeable
            _assemblyConfig = assemblyConfig;
            _workPipelines = new List<WorkPipeline>();

            try
            {
                DateTime _lastExecuteTime = LastExecuteTime;
                DateTime _nextExecuteTime = NextExecuteTime;
                PiplelineScheduleTime.SetScheduleTime(_assemblyConfig.ScheduleTime,out _scheduleTime, out _lastExecuteTime, ref _nextExecuteTime);
                LastExecuteTime = _lastExecuteTime;
                NextExecuteTime = _nextExecuteTime;
                InvokeStatus = PipelineInvokeStatus.Invokeable;
                Status = WorkPipelineStatus.Wait;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("The pipeline {0} set schedule failed, error message: {1}", Name, ex.Message));
                ExecutionMessage.Append(String.Format("The pipeline set schedule failed, error message: {0}", ex.Message));
                InvokeStatus = PipelineInvokeStatus.InvokeUnable;
                Status = WorkPipelineStatus.Failed;
            }
        }
        private void ExecuteAndUnloadAssembly()
        {
            if (!_isLoadAssembly)
            {
                PipelineAssemblyLoadContext assemblyLoadContext = new PipelineAssemblyLoadContext(_assemblyConfig.AssemblyPath);
                _pipelineAssembly = assemblyLoadContext.LoadFromAssemblyPath(_assemblyConfig.AssemblyPath);
                LoadPipelineTime = DateTime.Now;
                _isLoadAssembly = true;
            }
            try
            {
                // Load assembly
                // Get all types of the assembly.
                Type[] pipelineTypes = _pipelineAssembly.GetTypes();
                //Get pipline of inherit WorkPipeline class
                foreach (Type pipelineType in pipelineTypes)
                {              
                    if (pipelineType.BaseType.Name == typeof(WorkPipeline).Name)
                    {
                        object pipelineInstance = Activator.CreateInstance(pipelineType);
                        _workPipelines.Add((WorkPipeline)pipelineInstance);
                    }
                }
                // Execute pipeline
                ExecuteStartTime = DateTime.Now;
                Status = WorkPipelineStatus.Running;
                foreach(WorkPipeline workPipeline in _workPipelines)
                {
                    //workPipeline.Processor();
                }
                ExecuteEndTime = DateTime.Now;
                Status = WorkPipelineStatus.Success;
                _workPipelines.ForEach(pipeline =>
                {
                    if(pipeline.Status == WorkPipelineStatus.Failed)
                    {
                        Status = WorkPipelineStatus.Failed;
                    }
                });
                //Set next execute time.
                DateTime _lastExecuteTime = LastExecuteTime;
                DateTime _nextExecuteTime = NextExecuteTime;
                PiplelineScheduleTime.SetNextExecuteTime(_scheduleTime, out _lastExecuteTime, ref _nextExecuteTime);
                LastExecuteTime = _lastExecuteTime;
                NextExecuteTime = _nextExecuteTime;
                //Set pipeline can be invokeable
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("The pipeline {0} executed failed. detail error message: {1}", Name, ex.Message));
                Status = WorkPipelineStatus.Failed;
            }
            finally
            {
                InvokeStatus = PipelineInvokeStatus.Invokeable;
            }
        }
        private void RecordResultsExecution(object source, PipelineEventArgs args)
        {
            // Save executed info to database
            using (var context = new MySqlContext())
            {
                DockPipeline pipeline = context.DockPipelines.Find(this.Name);
                // First insert
                if (pipeline is null)
                {
                    pipeline = this;
                    context.DockPipelines.Add(pipeline);
                }
                else
                {
                    // Update row
                    pipeline.Status = this.Status;
                    pipeline.LastExecuteTime = this.LastExecuteTime;
                    pipeline.NextExecuteTime = this.NextExecuteTime;
                    pipeline.ExecuteStartTime = this.ExecuteStartTime;
                    pipeline.ExecuteEndTime = this.ExecuteEndTime;
                    pipeline.LoadPipelineTime = this.LoadPipelineTime;
                    pipeline.InvokeStatus = this.InvokeStatus;

                }
                context.SaveChanges();
            }
        }
        public override Task ExecuteAsync()
        {
            if (DateTime.Now < NextExecuteTime)
                return null;
            return Task.Run(() => {
                ExecuteAndUnloadAssembly();
            });
        }
    }
}
