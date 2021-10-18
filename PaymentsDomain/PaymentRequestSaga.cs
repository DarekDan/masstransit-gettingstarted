using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Saga;

namespace GettingStarted.PaymentsDomain
{
    public class PaymentRequestSaga : ISagaVersion, InitiatedBy<ISubmitPaymentRequest>, Orchestrates<IPaymentRequestAccepted>,
        Observes<IPaymentRequestCompleted, PaymentRequestSaga>
    {
        public Guid CorrelationId { get; set; }
        public DateTimeOffset? SubmitDate { get; set; }
        public DateTimeOffset? AcceptDate { get; set; }

        public DateTimeOffset? CompleteDate { get; set; }

        public async Task Consume(ConsumeContext<ISubmitPaymentRequest> context)
        {
            SubmitDate = context.Message.Submitted;
        }

        public async Task Consume(ConsumeContext<IPaymentRequestAccepted> context)
        {
            AcceptDate = context.Message.Accepted;
        }

        public async Task Consume(ConsumeContext<IPaymentRequestCompleted> context)
        {
            CompleteDate = context.Message.Completed;
        }

        public Expression<Func<PaymentRequestSaga, IPaymentRequestCompleted, bool>> CorrelationExpression =>
            (saga, message) => saga.CorrelationId == message.PaymentId;

        public int Version { get; set; }
    }
}