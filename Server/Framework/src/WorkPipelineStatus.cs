using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework
{
    public enum WorkPipelineStatus
    {
        Build = 1,
        Wait,
        Success,
        Executable,
        Running,
        Forbid,
        Failed
    }
}
