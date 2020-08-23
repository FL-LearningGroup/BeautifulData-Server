using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Runtime
{
    internal class WorkPipeline
    {
        private string _executeTime;
        private string _lastExecuteTime;
        private string _nextExecuteTime;
        private Assembly _assembly;

        public WorkPipeline(Assembly assembly)
        {
            _assembly = assembly;
        }

        public async Task StartPipeline()
        {

        }
        
    }
}
