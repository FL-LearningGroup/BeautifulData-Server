using BDS.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Runtime.Models
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    abstract internal class Pipeline
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public string Name { get; set; }
        public abstract DateTime ExecuteStartTime { get;}
        public abstract DateTime ExecuteEndTime { get;}
        public abstract DateTime LastExecuteTime { get; }
        public abstract DateTime NextExecuteTime { get; }
        public string ScheduleDateFormat { get { return "yyyy/MM/dd hh:mm:ss"; } }
        public abstract WorkPipelineStatus Status { get; }
        public abstract DateTime LoadPipelineTime { get; set; }
        public abstract DateTime UnloadPipelineTime { get; set; }
        public abstract InvokePipelineStatus InvokeStatus { get; set; }

        public Pipeline(string name)
        {
            Name = name;
        }
        public override bool Equals(Object obj)
        {
            return Name == ((Pipeline) obj).Name;
        }

        public abstract Task ExecuteAsync();
    }
}
