using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime.Models
{
    /// <summary>
    /// Assembly config status.
    /// </summary>
    internal static class PipelineAssemblyStatus
    {
        public const string ADD = "Add"; //Add assembly to server.
        public const string RUNNING = "Running"; // Assembly running by server.
        public const string DELETING = "Deleting"; //Deleting assembly
        public const string REMOVE = "Remove"; // Remove assembly in server.
    }
}
