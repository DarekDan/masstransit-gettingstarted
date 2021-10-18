using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GettingStarted.PaymentsDomain
{
    public class PaymentRequestConsumer : IConsumer<PaymentRequest>
    {
        private readonly ILogger<PaymentRequestConsumer> _logger;

        public PaymentRequestConsumer(ILogger<PaymentRequestConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<PaymentRequest> context)
        {
            _logger.LogInformation(
                "Received payment request: {@Message}",
                context.Message);
            return Task.CompletedTask;
        }
    }
}