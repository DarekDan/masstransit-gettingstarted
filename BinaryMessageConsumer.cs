using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GettingStarted
{

    public class BinaryMessage
    {
        public DateTimeOffset Now { get; set; }
    }
    public class BinaryMessageConsumer:IConsumer<BinaryMessage>
    {
        private readonly ILogger<BinaryMessageConsumer> _logger;

        public BinaryMessageConsumer(ILogger<BinaryMessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<BinaryMessage> context)
        {
            _logger.LogInformation("Received binary: {Now}", context.Message.Now);
            return Task.CompletedTask;
        }
    }
}