using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime.Models
{
    internal class PiplelineScheduleTime
    {
        public DateTime StartTime { get; set; }
        public PipelineScheduleApartTimeType Model { get; set; }
        public int ApartTime { get; set; }
    }
}
