using System;
using MassTransit;

namespace GettingStarted.PaymentsDomain
{
    public interface IPaymentRequestAccepted : CorrelatedBy<Guid>
    {
        DateTimeOffset Accepted { get; }
    }
}