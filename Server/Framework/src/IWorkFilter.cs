using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework
{
    [Obsolete("Forbid", true)]
    public interface IWorkFilter
    {
        string TagName { get; set; }
        string TagId { get; set; }
        string TagClass { get; set; }
    }
}
