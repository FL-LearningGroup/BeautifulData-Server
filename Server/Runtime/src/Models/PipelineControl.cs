using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BDS.Runtime.Models
{
    internal class PipelineControl
    {
        public Pipeline Resource { get; private set; }
        public CancellationTokenSource CancelTakenSource { get; private set; }
        public PipelineConfigStatus Status { get; set; }
        public DateTime LastExecuteDT { get; set; }
        public DateTime NextExecuteDT { get; set; }
        public PiplelineScheduleTimeOperation ScheduleTime { get; set; }
        public PipelineControl(Pipeline pipeline, PipelineConfigStatus status,DateTime lastExecuteDT, DateTime nextExecuteDT, PiplelineScheduleTimeOperation scheduleTime,CancellationTokenSource cancelTakenSource)
        {
            Resource = pipeline;
            Status = status;
            LastExecuteDT = lastExecuteDT;
            NextExecuteDT = nextExecuteDT;
            ScheduleTime = scheduleTime;
            CancelTakenSource = cancelTakenSource;
        }
        public async Task InvokePipeline()
        {
            await Resource.ExecuteAsync(CancelTakenSource.Token);
        }
    }
}
