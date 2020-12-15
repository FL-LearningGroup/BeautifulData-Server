using BDS.Runtime.Respository;
using BDS.Runtime.Respository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public DateTime LastExecuteDT { get; private set; }
        public DateTime NextExecuteDT { get; private set; }
        public PipelineTaskResult TaskResult { get; private set; }
        public PiplelineScheduleTimeOperation ScheduleTime { get; private set; }
        public PipelineControl(Pipeline pipeline, PipelineConfigStatus status,DateTime lastExecuteDT, DateTime nextExecuteDT, PiplelineScheduleTimeOperation scheduleTime,CancellationTokenSource cancelTakenSource)
        {
            Resource = pipeline;
            Status = status;
            LastExecuteDT = lastExecuteDT;
            NextExecuteDT = nextExecuteDT;
            ScheduleTime = scheduleTime;
            CancelTakenSource = cancelTakenSource;
        }
        public async Task InvokePipelineAsync()
        {
            if ((DateTime.Now >= NextExecuteDT) && (Status ==  PipelineConfigStatus.Running))
            {
                TaskResult = await Resource.ExecuteAsync(CancelTakenSource.Token);
            }
            lock (this)
            {
                if (TaskResult.Status == Framework.WorkPipelineStatus.Success)
                {
                    SetExecuteSchedule();
                    UpdatePipelineConfig();
                }
                Status = PipelineConfigStatus.Wait;
            }
        }

        public void SetExecuteSchedule()
        {
            DateTime lastExecuteDT = LastExecuteDT;
            DateTime nextExecuteDT = NextExecuteDT;
            ScheduleTime.SetNextExecuteTime(ref lastExecuteDT, ref nextExecuteDT);
            LastExecuteDT = lastExecuteDT;
            NextExecuteDT = nextExecuteDT;
        }

        public void UpdatePipelineConfig()
        {
            using(MySqlContext context = new MySqlContext())
            {
                PipelineConfig pipelinConfig = context.PipelineConfig.FirstOrDefault(item => item.Name == Resource.Name);
                pipelinConfig.LastExecuteDT = LastExecuteDT;
                pipelinConfig.NextExecuteDT = NextExecuteDT;
                pipelinConfig.Status = PipelineConfigStatus.Wait;
                context.PipelineConfig.Update(pipelinConfig);
                context.SaveChanges();
            }
        }
    }
}
