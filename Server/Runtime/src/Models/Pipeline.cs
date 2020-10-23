using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Runtime.Models
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    abstract internal class Pipeline: IPipeline
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public string Name { get; set; }
        public string ExecuteStartTime { get;}
        public string ExecuteEndTime { get;}
        public PipelineStatus Status { get; }

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
