using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework
{
    public class WorkSiteStatusTriggerEventArgs: EventArgs
    {
        public WorkSiteStatus Status { get; set; }
    }
}
