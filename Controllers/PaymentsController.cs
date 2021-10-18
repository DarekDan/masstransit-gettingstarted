using System;
using GettingStarted.PaymentsDomain;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NodaMoney;

namespace GettingStarted.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IBus _bus;

        public PaymentsController(ILogger<PaymentsController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost]
        public IActionResult Create(PayOrder payOrder)
        {
            _logger.LogInformation("Receive payment order {@PayOrder}", payOrder);
            var paymentRequest = new PaymentRequest
            {
                RequestId = Guid.NewGuid(), AccountNumber = payOrder.AccountNumber,
                Amount = new Money(payOrder.Amount, Currency.FromCode(payOrder.Currency)),
                RequestedDateTime = payOrder.Immediate ? DateTimeOffset.Now : payOrder.At
            };
            _bus.Publish(paymentRequest).Wait();
            return Ok();
        }
    }
}