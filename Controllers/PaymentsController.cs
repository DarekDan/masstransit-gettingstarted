using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GettingStarted.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(ILogger<PaymentsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create(PaymentRequest paymentRequest)
        {
            _logger.LogInformation("Receive payment request {@PaymentRequest}", paymentRequest);
            return Ok();
        }
    }
}