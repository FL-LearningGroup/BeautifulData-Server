using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BDS.Runtime.Models;
using BDS.Runtime.Respository;

namespace BDS.Runtime
{
    public class BDS
    {
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Main(string[] args)
        {
            Logger.Info("Start up the BDS Server");
            CreateHostBuilder(args).Build().Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    //Get Mysql configuration.
#if DEBUG
                    MySqlConfiguration mySqlConfiguration = hostContext.Configuration.GetSection("MySqlDebug").Get<MySqlConfiguration>();
#else
                    MySqlConfiguration mySqlConfiguration = hostContext.Configuration.GetSection("MySql").Get<MySqlConfiguration>();
#endif
                    services.AddHostedService<PipelineHostService>();
                });
    }
}
