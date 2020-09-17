namespace BDS.CollectData
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BDS.CollectData.Models;
    public class WorkSiteStatusEventArgs: EventArgs
    {
        public WorkSiteStatus Status { get; set; }
    }
}
