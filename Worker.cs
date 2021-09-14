using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GettingStarted
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new Message { Text = $"The time is {DateTimeOffset.Now}" }, stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
