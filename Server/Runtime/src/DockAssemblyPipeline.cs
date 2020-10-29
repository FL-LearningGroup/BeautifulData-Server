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
    internal class DockAssemblyPipeline: Pipeline
    {
        private PipelineAssemblyConfig _assemblyConfig;
        private WorkPipelineStatus _status;
        private PiplelineScheduleTime _scheduleTime;
        private DateTime _lastExecuteTime;
        private DateTime _nextExecuteTime;
        private DateTime _executeStartTime;
        private DateTime _executeEndTime;
        private DateTime _loadAssemblyTime;
        private DateTime _unloadPipelineTime;
        private Assembly _pipelineAssembly;
        private bool _isLoadAssembly = false;
        private List<WorkPipeline> _workPipelines;
        public PipelineAssemblyConfig AssemblyConfig { get { return _assemblyConfig; } }
        public override WorkPipelineStatus Status { get { return _status; } }
        public PiplelineScheduleTime ScheduleTime { get { return _scheduleTime; } }
        public override DateTime LastExecuteTime { get { return _lastExecuteTime; } }
        public override DateTime NextExecuteTime { get { return _nextExecuteTime; } }
        public override DateTime ExecuteStartTime { get { return _executeStartTime; } }
        public override DateTime ExecuteEndTime { get { return _executeEndTime; } }
        public override DateTime LoadPipelineTime { get { return _loadAssemblyTime; } set { _loadAssemblyTime = value; } }
        public override DateTime UnloadPipelineTime { get { return _unloadPipelineTime; } set { _unloadPipelineTime = value; } }
        public override InvokePipelineStatus InvokeStatus { get; set; }
        public DockAssemblyPipeline(PipelineAssemblyConfig assemblyConfig): base(assemblyConfig.AssemblyKey)
        {
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
                    workPipeline.Processor();
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
                InvokeStatus = InvokePipelineStatus.Invokeable;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("The pipeline {0} executed failed. detail error message: {1}", Name, ex.Message));
                _status = WorkPipelineStatus.Failed;
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
