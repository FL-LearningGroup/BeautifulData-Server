using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime
{
    internal interface IPipelineWorker
    {
        public List<AssemblyConfig> AssemblyConfigList { get; }
        public List<AssemblyConfig> AddAssemblyConfigList { get; }
        public List<AssemblyConfig> RemoveAssemblyConfigList { get; }

        public void AddPipeline();
        public void RemovePipeline();
    }
}
