using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTasks.HostedServices
{
    public class UpdateConfigService : IHostedService, IDisposable
    {
        private readonly ILogger<UpdateConfigService> _logger;

        private Timer _timer;

        private readonly string _configPath = "dynamicConfiguration.json";

        private readonly Random _random = new Random();


        public UpdateConfigService(ILogger<UpdateConfigService> logger)
        {
            _logger = logger;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(UpdateConfigService)} StartAsync.");

            _timer = new Timer(UpdateConfig, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(11));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(UpdateConfigService)} StopAsync.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void UpdateConfig(object state)
        {
            if(File.Exists(_configPath))
            {
                var number = _random.Next(0, 99);
                var json = JsonSerializer.Serialize(new { RandomNumber = number });
                File.WriteAllText(_configPath, json);

                _logger.LogInformation($"Config updated with random number {number}.");
            }
            else
            {
                _logger.LogInformation("Config not found.");
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
