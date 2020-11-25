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
        protected PipelineAssemblyConfig assemblyConfig;
        protected PiplelineScheduleTime scheduleTime;
        protected Assembly pipelineAssembly;
        protected PipelineAssemblyLoadContext assemblyLoadContext;
        protected List<WorkPipeline> workPipelines;
        [NotMapped]
        public PipelineAssemblyConfig AssemblyConfig { get { return assemblyConfig; } private set { } }
        [NotMapped]
        public PiplelineScheduleTime ScheduleTime { get { return scheduleTime; } private set { } }
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
