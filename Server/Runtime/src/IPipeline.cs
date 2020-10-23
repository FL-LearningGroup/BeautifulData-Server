using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Runtime
{
    public interface IPipeline
    {
        public string Name { get; set; }
        public string ExecuteStartTime { get; }
        public string ExecuteEndTime { get; }
        public PipelineStatus Status { get; }
        public Task ExecuteAsync();
    }
}
