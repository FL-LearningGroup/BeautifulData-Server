using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using workservice.powershell.Model;

namespace workservice.powershell
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DataMemory.Data.Add(new ObjectData { Order = 1, Message = "key1" });
            DataMemory.Data.Add(new ObjectData { Order = 2, Message = "key2" });
            while (!stoppingToken.IsCancellationRequested)
            {
                DataMemory.Data.ForEach(d => Console.WriteLine(String.Format("Order {0}, Message {1}", d.Order, d.Message)));
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
