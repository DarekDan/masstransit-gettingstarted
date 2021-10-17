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
            await Task.Factory.StartNew(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await _bus.Publish(new Message { Text = $"The time is {DateTimeOffset.Now}" }, stoppingToken);
                    await Task.Delay(new Random().Next(10), stoppingToken);
                }
            }, stoppingToken);
            await Task.Factory.StartNew(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await _bus.Publish(new BinaryMessage { Now = DateTimeOffset.Now }, stoppingToken);
                    await Task.Delay(new Random().Next(10), stoppingToken);
                }
            }, stoppingToken);
            await Task.Factory.StartNew(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await _bus.Publish(new Eva { Text = Guid.NewGuid().ToString()}, stoppingToken);
                    await Task.Delay(new Random().Next(10), stoppingToken);
                }
            }, stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
            _logger.LogInformation("Worker stopped at: {Time}", DateTimeOffset.Now);
        }
    }
}