using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BDS.Runtime.Models;

namespace BDS.Runtime
{
    internal class DockAssemblyPipeline: Pipeline
    {
        private PipelineAssemblyConfig _assemblyConfig;
        private PipelineStatus _status;
        public PipelineStatus Status { get { return _status; } }
        public PipelineAssemblyConfig AssemblyConfig { get { return _assemblyConfig; } }
        public DockAssemblyPipeline(PipelineAssemblyConfig assemblyConfig): base(assemblyConfig.AssemblyKey)
        {
            _status = PipelineStatus.Wait;
            _assemblyConfig = assemblyConfig;
        }
        public override async Task ExecuteAsync()
        {
            Logger.Info(String.Format("Pipeline Name: {0} exectute.", Name));
            await Task.Delay(3000);
        }
    }
}
