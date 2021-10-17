using System;
using System.ComponentModel.DataAnnotations;

namespace GettingStarted
{
    public class PayOrder
    {
        [Required(ErrorMessage = "An account number must be provided")]
        public String AccountNumber { get; set; }
        [Required(ErrorMessage = "An amount must be specified")]
        public Decimal Amount { get; set; }
        [Required(ErrorMessage = "A currency code must be provided")]
        public String Currency { get; set; }
        public Boolean Immediate { get; set; }
        public DateTimeOffset At { get; set; }
    }
}