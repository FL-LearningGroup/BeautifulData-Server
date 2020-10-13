using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework
{
    public enum WorkPipelineStatus
    {
        Build = 1,
        Success,
        Executable,
        Forbid,
        Failed
    }
}
