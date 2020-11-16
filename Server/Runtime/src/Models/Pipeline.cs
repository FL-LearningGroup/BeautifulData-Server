using BDS.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace BDS.Runtime.Models
{
    /// <summary>
    /// Abstract pipline as base class.
    /// </summary>
    /// <designspec>
    /// ============
    /// 1. Record pipline Name
    /// 2. Record pipeline status
    /// 3. Record pipeline invoke status
    /// 4. Record pipeline execute time
    /// 6. Record pipeline load time
    /// 5. Invoke pipeline
    /// 6. Sent event as pipeline status
    /// ========
    /// </designspec>
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    internal abstract class Pipeline
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        protected WorkPipelineStatus status;
        public event EventHandler<PipelineEventArgs> StatusEvent;
        public string Name { get; set; }
        public WorkPipelineStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                if (StatusEvent != null)
                {
                    PipelineEventArgs args = new PipelineEventArgs();
                    args.Status = status;
                    StatusEvent(this, args);
                }
            }
        }

        public PipelineInvokeStatus InvokeStatus { get; set; }
        public DateTime LoadPipelineDT { get; set; }
        public DateTime UnloadPipelineDT { get; set; }
        public DateTime ExecuteStartDT { get; set; }
        public DateTime ExecuteEndDT { get; set;}
        public StringBuilder ExecutionMessage { get; set; }
        public override bool Equals(Object obj)
        {
            return Name == ((Pipeline) obj).Name;
        }

        public abstract Task ExecuteAsync();
    }
}
