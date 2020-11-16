using BDS.Framework;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BDS.Runtime.Models
{
    /// <summary>
    /// Represent dock pipeline execution history
    /// </summary>
    internal class DockPipelineHistory
    {
        public string Name { get; set; }
        public WorkPipelineStatus Status { get; set; }
        public PipelineInvokeStatus InvokeStatus { get; set; }
        public DateTime LoadPipelineDT { get; set; }
        public DateTime UnloadPipelineDT { get; set; }
        public DateTime ExecuteStartDT { get; set; }
        public DateTime ExecuteEndDT { get; set; }
        public DateTime LastExecuteDT { get; set; }
        public DateTime NextExecuteDT { get; set; }
        public StringBuilder ExecutionMessage { get; set; }
        public DateTime InsertRowDT { get; set; }
        public DockPipelineHistory()
        {
            ExecutionMessage = new StringBuilder(1024, 1024 * 10);
        }
    }
}
