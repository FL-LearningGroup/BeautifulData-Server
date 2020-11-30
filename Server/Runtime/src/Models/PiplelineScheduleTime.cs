using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime.Models
{
    internal class PiplelineScheduleTime
    {
        public PiplelineScheduleTime(PipelineScheduleApartTimeType model, int apartTime)
        {
            Model = model;
            ApartTime = apartTime;
        }
        public PipelineScheduleApartTimeType Model { get; set; }
        public int ApartTime { get; set; }
    }
}
