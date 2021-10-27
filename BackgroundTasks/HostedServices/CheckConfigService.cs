using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTasks.HostedServices
{
    public class CheckConfigService : BackgroundService
    {
        private readonly ILogger<CheckConfigService> _logger;

        private readonly string _configPath = "dynamicConfiguration.json";


        public CheckConfigService(ILogger<CheckConfigService> logger)
        {
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000, stoppingToken);

                var config = await File.ReadAllTextAsync(_configPath, stoppingToken);

                _logger.LogInformation($"{nameof(CheckConfigService)}: {config}");
            }
        }
    }
}
