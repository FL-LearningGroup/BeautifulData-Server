using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BDS.Runtime
{
    public class PipelineWorker : BackgroundService
    {
        public PipelineWorker()
        {
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var DLL = Assembly.LoadFrom(@"D:\Lucas\git\BeautifulData-Server\server\pipeline\fuyang\src\bin\Debug\netcoreapp3.1\BDS.Pipeline.FuYang.dll");
                foreach (Type type in DLL.GetExportedTypes())
                {
                    if (type.Name.Contains("Pipeline_"))
                    {
                        var pipeline = Activator.CreateInstance(type);
                        type.InvokeMember("StartWork", BindingFlags.InvokeMethod, null, pipeline, null);
                        //break;
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
