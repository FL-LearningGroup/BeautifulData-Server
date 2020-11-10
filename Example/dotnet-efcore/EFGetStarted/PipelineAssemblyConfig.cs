using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFGetStarted
{
    public class PipelineAssemblyConfig
    {
        [Key]
        public string AssemblyKey { get; set; }
        public string AssemblyPath { get; set; }
        public string AssemblyStatus { get; set; }
        public string ScheduleTime { get; set; }
        public PipelineAssemblyConfig()
        {

        }

        public override bool Equals(Object obj)
        {
            return AssemblyKey == ((PipelineAssemblyConfig)obj).AssemblyKey;
        }

    }
}
