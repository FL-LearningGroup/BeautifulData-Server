using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime
{
    internal static class AssemblyConfigStatus
    {
        public const string ADD = "Add";
        public const string RUNNING = "Running";
        public const string DELETING = "Deleting";
        public const string REMOVE = "Remove";
    }
    [Serializable]
    internal class AssemblyConfig
    {
        public string AssemblyKey { get; set; }
        public string AssemblyPath { get; set; }
        public string AssemplyStatus { get; set; }
    }
}
