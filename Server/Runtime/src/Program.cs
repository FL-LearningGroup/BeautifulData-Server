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
using BDS.Runtime.DataBase;

namespace BDS.Runtime
{
    public class Program
    {
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Main(string[] args)
        {
            Logger.Info(String.Format("Start the BDS Server, {0}.", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
            CreateHostBuilder(args).Build().Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    //Get Mysql configuration.
                    MySqlConfiguration mySqlConfiguration = hostContext.Configuration.GetSection("MySql").Get<MySqlConfiguration>();
                    services.AddHostedService<PipelineHostService>();
                });
    }
}
