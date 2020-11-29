using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BDS.Runtime.Models
{
    internal class PipelineControl
    {
        public Pipeline Resource { get; private set; }
        public CancellationTokenSource CancelTakenSource { get; private set; }

        public PipelineControl(Pipeline pipeline, CancellationTokenSource cancelTakenSource)
        {
            Resource = pipeline;
            CancelTakenSource = cancelTakenSource;
        }
    }
}
