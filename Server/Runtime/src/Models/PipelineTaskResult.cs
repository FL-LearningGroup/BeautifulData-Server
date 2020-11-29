using BDS.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime.Models
{
    public class PipelineTaskResult
    {
        public string Name { get; set; }
        public WorkPipelineStatus Status { get; set; }
    }
}
