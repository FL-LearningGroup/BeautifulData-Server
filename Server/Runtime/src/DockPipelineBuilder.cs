using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BDS.Runtime.Models;
using BDS.Framework;
using log4net.Util;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace BDS.Runtime
{
    internal class DockPipelineBuilder: DockPipeline
    {
        public DockPipelineBuilder(PipelineAssemblyConfig assemblyConfig)
        {
            Name = assemblyConfig.AssemblyKey;
            _status = WorkPipelineStatus.Wait;
            //Set pipeline can be invokeable
            InvokeStatus = InvokePipelineStatus.Invokeable;
            _assemblyConfig = assemblyConfig;
            _workPipelines = new List<WorkPipeline>();
            try
            {
                PiplelineScheduleTime.SetScheduleTime(_assemblyConfig.ScheduleTime,out _scheduleTime, out _lastExecuteTime, ref _nextExecuteTime);
            }
            catch(Exception ex)
            {
                _status = WorkPipelineStatus.Failed;
                Logger.Error(String.Format("The pipeline {0} set schedule failed, error message: {1}", Name, ex.Message));
            }
        }
        private void ExecuteAndUnloadAssembly()
        {
            if (!_isLoadAssembly)
            {
                PipelineAssemblyLoadContext assemblyLoadContext = new PipelineAssemblyLoadContext(_assemblyConfig.AssemblyPath);
                _pipelineAssembly = assemblyLoadContext.LoadFromAssemblyPath(_assemblyConfig.AssemblyPath);
                _loadAssemblyTime = DateTime.Now;
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
                _executeStartTime = DateTime.Now;
                _status = WorkPipelineStatus.Running;
                foreach(WorkPipeline workPipeline in _workPipelines)
                {
                    //workPipeline.Processor();
                }
                _executeEndTime = DateTime.Now;
                _status = WorkPipelineStatus.Success;
                _workPipelines.ForEach(pipeline =>
                {
                    if(pipeline.Status == WorkPipelineStatus.Failed)
                    {
                        _status = WorkPipelineStatus.Failed;
                    }
                });
                //Set next execute time.
                PiplelineScheduleTime.SetNextExecuteTime(_scheduleTime, out _lastExecuteTime, ref _nextExecuteTime);
                //Set pipeline can be invokeable
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("The pipeline {0} executed failed. detail error message: {1}", Name, ex.Message));
                _status = WorkPipelineStatus.Failed;
            }
            finally
            {
                // Save executed info to database
                using (var context = new MySqlContext())
                {
                    DockPipeline pipeline = context.DockPipelines.Find(this.Name);
                    if (pipeline is null)
                    {
                        pipeline = this;
                        context.DockPipelines.Add(pipeline);
                    }
                    else
                    {
                        pipeline.Status = this.Status;
                        pipeline.LastExecuteTime = this.LastExecuteTime;
                        pipeline.NextExecuteTime = this.NextExecuteTime;
                        pipeline.ExecuteStartTime = this.ExecuteStartTime;
                        pipeline.ExecuteEndTime = this.ExecuteEndTime;
                        pipeline.InvokeStatus = this.InvokeStatus;
                    }
                    context.SaveChanges();
                }
                InvokeStatus = InvokePipelineStatus.Invokeable;
            }
        }
        public override Task ExecuteAsync()
        {
            return Task.Run(() => {
                ExecuteAndUnloadAssembly();
            });
        }
    }
}
