using System;
using NodaMoney;

namespace GettingStarted.PaymentsDomain
{
    public class PaymentRequest
    {
        public DateTimeOffset RequestedDateTime { get; set; }
        public Guid RequestId { get; set; }
        public String AccountNumber { get; set; }
        public Money Amount { get; set; }
    }
}