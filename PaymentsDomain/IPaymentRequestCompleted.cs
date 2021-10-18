using System;

namespace GettingStarted.PaymentsDomain
{
    public interface IPaymentRequestCompleted
    {
        Guid PaymentId { get; }
        DateTimeOffset Completed { get; }
    }
}