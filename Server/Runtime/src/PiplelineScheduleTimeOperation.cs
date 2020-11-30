using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BDS.Runtime.Models;

namespace BDS.Runtime
{
    /// <summary>
    /// Pipeline execute schedule time
    /// </summary>
    [Serializable]
    internal class PiplelineScheduleTimeOperation: PiplelineScheduleTime
    {
        public PiplelineScheduleTimeOperation(PipelineScheduleApartTimeType model, int apartTime): base(model, apartTime)
        {

        }

        public  void SetNextExecuteTime(out DateTime lastExecuteTime, ref DateTime nextExecuteTime)
        {
            lastExecuteTime = nextExecuteTime;
            if (Model == PipelineScheduleApartTimeType.Y)
            {
                nextExecuteTime = lastExecuteTime.AddMonths(12);
                return;
            }
            if (Model == PipelineScheduleApartTimeType.M)
            {
                nextExecuteTime = lastExecuteTime.AddMonths(ApartTime);
                return;
            }
            if (Model == PipelineScheduleApartTimeType.W)
            {
                nextExecuteTime = lastExecuteTime.AddDays(7);
                return;
            }
            if (Model == PipelineScheduleApartTimeType.D)
            {
                nextExecuteTime = lastExecuteTime.AddDays(ApartTime);
                return;
            }
            if (Model == PipelineScheduleApartTimeType.HH)
            {
                nextExecuteTime = lastExecuteTime.AddHours(ApartTime);
                return;
            }
            if (Model == PipelineScheduleApartTimeType.MM)
            {
                nextExecuteTime = lastExecuteTime.AddMinutes(ApartTime);
                return;
            }
            if (Model == PipelineScheduleApartTimeType.SS)
            {
                nextExecuteTime = lastExecuteTime.AddSeconds(ApartTime);
                return;
            }
        }
    }
}
