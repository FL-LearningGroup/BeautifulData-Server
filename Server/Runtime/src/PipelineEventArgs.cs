using BDS.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime
{
    public class PipelineEventArgs: EventArgs
    {
        public WorkPipelineStatus Status { get; set; }
        public PipelineInvokeStatus InvokeStatus { get; set; }
    }
}
