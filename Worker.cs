using System;
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
            /*
            await Task.Factory.StartNew(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await _bus.Publish(
                        new PaymentRequest
                        {
                            RequestedDateTime = DateTimeOffset.Now, RequestId = Guid.NewGuid(),
                            AccountNumber = RandomGenerator.RandomAccountNumber(12),
                            Amount = RandomGenerator.RandomAmount(1000)
                        }, stoppingToken);
                    await Task.Delay(new Random().Next(1000, 2000), stoppingToken);
                }
            }, stoppingToken);
            */
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}