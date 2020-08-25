using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Runtime
{
    internal class Pipeline
    {
        private string _executeTime;
        private string _lastExecuteTime;
        private string _nextExecuteTime;
        private string _fullName;
        private string _loadDate;
        private string _assemblyPath;
        private Assembly _assembly;

        public string FullName { get { return _fullName; } }
        public string LoadDate { get { return _loadDate; } }
        public string AssemblyPath { get { return _assemblyPath; } }
        public PipelineStatus status { get; set; }

        public Pipeline(Assembly assembly, string assemblyPath, string fullName, string loadDate)
        {
            _assembly = assembly;
            _assemblyPath = assemblyPath;
            _fullName = fullName;
            _loadDate = loadDate;

        }

        public async Task StartPipeline()
        {

        }
        
    }
}
