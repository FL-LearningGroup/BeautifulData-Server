using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime
{
    /// <summary>
    /// Interface pipeline worker.
    /// </summary>
    internal interface IPipelineWorker
    {
        /// <summary>
        /// Exists assembly in pipeline worker.
        /// </summary>
        public List<AssemblyConfig> AssemblyConfigList { get; }
        /// <summary>
        /// Add assembly list in pipeline worker.
        /// </summary>
        public List<AssemblyConfig> AddAssemblyConfigList { get; }
        /// <summary>
        /// Remove assembly list in pipeline worker.
        /// </summary>
        public List<AssemblyConfig> RemoveAssemblyConfigList { get; }

        /// <summary>
        /// Add pipeline
        /// </summary>
        public void AddPipeline();
        /// <summary>
        /// Remove pipeline
        /// </summary>
        public void RemovePipeline();
    }
}
