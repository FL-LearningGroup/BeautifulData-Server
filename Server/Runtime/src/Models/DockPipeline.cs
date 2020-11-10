using BDS.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Runtime.Models
{
    internal class DockPipeline : Pipeline
    {
        protected PipelineAssemblyConfig _assemblyConfig;
        protected WorkPipelineStatus _status;
        protected PiplelineScheduleTime _scheduleTime;
        protected DateTime _lastExecuteTime;
        protected DateTime _nextExecuteTime;
        protected DateTime _executeStartTime;
        protected DateTime _executeEndTime;
        protected DateTime _loadAssemblyTime;
        protected DateTime _unloadPipelineTime;
        protected Assembly _pipelineAssembly;
        protected bool _isLoadAssembly = false;
        protected List<WorkPipeline> _workPipelines;
        [Column("AssemblyKey")]
        public PipelineAssemblyConfig AssemblyConfig { get { return _assemblyConfig; }  private set { } }
        public override WorkPipelineStatus Status { get { return _status; } set { _status = value; }}
        [NotMapped]
        public PiplelineScheduleTime ScheduleTime { get { return _scheduleTime; } private set { } }
        public override DateTime LastExecuteTime { get { return _lastExecuteTime; } set { _lastExecuteTime = value; } }
        public override DateTime NextExecuteTime { get { return _nextExecuteTime; } set { _nextExecuteTime = value; } }
        public override DateTime ExecuteStartTime { get { return _executeStartTime; } set { _executeStartTime = value; } }
        public override DateTime ExecuteEndTime { get { return _executeEndTime; } set { _executeEndTime = value; } }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public override DateTime LoadPipelineTime { get { return _loadAssemblyTime; } set { _loadAssemblyTime = value; } }
        
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public override DateTime UnloadPipelineTime { get { return _unloadPipelineTime; } set { _unloadPipelineTime = value; } }
        public override InvokePipelineStatus InvokeStatus { get; set; }

        public DockPipeline()
        {

        }
        public override Task ExecuteAsync()
        {
            return new Task(null);
        }
    }
}
