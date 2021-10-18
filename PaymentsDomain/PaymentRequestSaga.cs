using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Saga;
using Microsoft.Extensions.Logging;

namespace GettingStarted.PaymentsDomain
{
    public class PaymentRequestSaga : ISagaVersion, InitiatedBy<ISubmitPaymentRequest>, Orchestrates<IPaymentRequestAccepted>,
        Observes<IPaymentRequestCompleted, PaymentRequestSaga>
    {
        private readonly ILogger _logger;

        public PaymentRequestSaga(ILogger<PaymentRequestSaga> logger)
        {
            _logger = logger;
        }
        public Guid CorrelationId { get; set; }
        public DateTimeOffset? SubmitDate { get; set; }
        public DateTimeOffset? AcceptDate { get; set; }

        public DateTimeOffset? CompleteDate { get; set; }

        public async Task Consume(ConsumeContext<ISubmitPaymentRequest> context)
        {
            _logger.LogInformation("SAGA Received payment request {@Message}", context.Message);
            SubmitDate = context.Message.Submitted;
        }

        public async Task Consume(ConsumeContext<IPaymentRequestAccepted> context)
        {
            _logger.LogInformation("SAGA Received payment acceptance {@Message}", context.Message);
            AcceptDate = context.Message.Accepted;
        }

        public async Task Consume(ConsumeContext<IPaymentRequestCompleted> context)
        {
            _logger.LogInformation("SAGA Completed payment request {@Message}", context.Message);
            CompleteDate = context.Message.Completed;
        }

        public Expression<Func<PaymentRequestSaga, IPaymentRequestCompleted, bool>> CorrelationExpression =>
            (saga, message) => saga.CorrelationId == message.PaymentId;

        public int Version { get; set; }
    }
}