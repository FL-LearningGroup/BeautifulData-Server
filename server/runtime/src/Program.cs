using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BDS.Runtime
{
    public class Program
    {
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Main(string[] args)
        {
            Logger.Info("Start up BDS service.");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<PipelineHostService>();
                });
    }
}
