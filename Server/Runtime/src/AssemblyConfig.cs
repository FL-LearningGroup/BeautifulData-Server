using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime
{
    /// <summary>
    /// Assembly config status.
    /// </summary>
    internal static class AssemblyConfigStatus
    {
        public const string ADD = "Add"; //Add assembly to server.
        public const string RUNNING = "Running"; // Assembly running by server.
        public const string DELETING = "Deleting"; //Deleting assembly
        public const string REMOVE = "Remove"; // Remove assembly in server.
    }
    [Serializable]
    internal class AssemblyConfig: IEquatable<AssemblyConfig>
    {
        /// <summary>
        /// Mark assembly as unique.
        /// </summary>
        public string AssemblyKey { get; set; }
        /// <summary>
        /// Store path of assembly.
        /// </summary>
        public string AssemblyPath { get; set; }

        /// <summary>
        /// Display the assembly status of the status in the server.
        /// </summary>
        public string AssemblyStatus { get; set; }

        /// <summary>
        /// Execute time of assembly. 
        /// </summary>
        public string ScheduleTime { get; set; }

        public bool Equals(AssemblyConfig config)
        {
            return this.AssemblyKey == config.AssemblyKey;
        }
    }

}
