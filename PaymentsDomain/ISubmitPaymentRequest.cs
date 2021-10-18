using System;
using MassTransit;

namespace GettingStarted.PaymentsDomain
{
    public interface ISubmitPaymentRequest : CorrelatedBy<Guid>
    {
        DateTimeOffset Submitted { get; }
    }
}