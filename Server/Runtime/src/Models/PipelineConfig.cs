using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime.Models
{
    internal class PipelineConfig
    {
        public string Name { get; set; }
        public PipelineConfigStatus Status { get; set; }

        public PipelineReferenceType Type { get; set; }

        public PipelineReferenceAddressType AddressType { get; set; }

        public string PipelineReferenceAddress { get; set; }

        public DateTime StartDT { get; set; }

        public PipelineScheduleApartTimeType ApartTimeType { get; set; }

        public int ApartTime { get; set; }
    }
}
