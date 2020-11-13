using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BDS.Runtime.Models
{
    /// <summary>
    /// Represent dock pipeline execution history
    /// </summary>
    internal class DockPipelineHistory: DockPipeline
    {
        public DateTime InsertRowDT { get; set; }
    }
}
