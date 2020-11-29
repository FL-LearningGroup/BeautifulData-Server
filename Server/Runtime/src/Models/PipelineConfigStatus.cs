using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime.Models
{
    /// <summary>
    /// Pipeline config invloke flow
    /// </summary>
    /// <flow>
    /// Add(manual)-->Wait(auto>-->Running(auto)
    /// Stop(manual) --> Stopped(auto)
    /// Remove(manual) --> Removed(auto)
    /// </flow>
    enum PipelineConfigStatus
    {
        Add = 1,
        Wait,
        Running,
        Stop,
        Stopped,
        Remove,
        Removed,
    }
}
