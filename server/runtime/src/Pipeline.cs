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
        private Assembly _assembly;

        public string FullName { get; }
        public string LoadDate { get; }
        public PipelineStatus status { get; set; }

        public Pipeline(Assembly assembly, string fullName, string loadDate)
        {
            _assembly = assembly;
            FullName = fullName;
            LoadDate = loadDate;
        }

        public async Task StartPipeline()
        {

        }
        
    }
}
