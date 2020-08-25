using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime
{
    public interface IPipelineWorker
    {
        public List<string> AssemblyPathList { get; }
        public List<string> WaitAssemblyPathList { get; }
        public void LoadAssembly();
        public void UnloadAssembly();
    }
}
