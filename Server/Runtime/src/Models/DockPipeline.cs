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
    /// <summary>
    /// Represent pipeline that locad through dll file.
    /// </summary>
    /// <designspec>
    /// 1. Recore schedule time
    /// 2. Record last and next time.
    /// </designspec>
    internal class DockPipeline : Pipeline
    {
        protected PipelineAssemblyConfig _assemblyConfig;
        protected PiplelineScheduleTime _scheduleTime;
        protected Assembly _pipelineAssembly;
        protected bool _isLoadAssembly = false;
        protected List<WorkPipeline> _workPipelines;
        [Column("AssemblyKey")]
        public PipelineAssemblyConfig AssemblyConfig { get { return _assemblyConfig; } private set { } }
        [NotMapped]
        public PiplelineScheduleTime ScheduleTime { get { return _scheduleTime; } private set { } }
        public  DateTime LastExecuteTime { get; set; }
        public  DateTime NextExecuteTime { get; set; }
        public DockPipeline()
        {
            ExecutionMessage = new StringBuilder(1024, 1024*10);
        }
        public override Task ExecuteAsync()
        {
            return new Task(null);
        }
    }
}
