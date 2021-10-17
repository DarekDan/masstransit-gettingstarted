using System.Net.Mime;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GettingStarted
{

    public class Eva
    {
        public string Text { get; set; }
    }
    public class EvaConsumer:IConsumer<Eva>
    {
        private readonly ILogger<EvaConsumer> _logger;

        public EvaConsumer(ILogger<EvaConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Eva> context)
        {
            _logger.LogInformation("Received Eva.Text: {Text} with {HasValue}", context.Message.Text, context.CorrelationId.HasValue);
            return Task.CompletedTask;
        }
    }
}