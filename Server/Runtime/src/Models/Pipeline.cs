using BDS.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Runtime.Models
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    internal abstract class Pipeline
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        [Key]
        public string Name { get; set; }
        public abstract DateTime LastExecuteTime { get; set; }
        public abstract DateTime NextExecuteTime { get; set; }
        public abstract DateTime ExecuteStartTime { get; set; }
        public abstract DateTime ExecuteEndTime { get; set;}
        public abstract WorkPipelineStatus Status { get; set; }
        public abstract DateTime LoadPipelineTime { get; set; }
        public abstract DateTime UnloadPipelineTime { get; set; }
        public abstract InvokePipelineStatus InvokeStatus { get; set; }

        public override bool Equals(Object obj)
        {
            return Name == ((Pipeline) obj).Name;
        }

        public abstract Task ExecuteAsync();
    }
}
