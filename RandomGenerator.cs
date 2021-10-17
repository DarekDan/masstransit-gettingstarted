using System;
using System.Linq;

namespace GettingStarted
{
    public class RandomGenerator
    {
        private static readonly Random CoreRandom = new Random();

        public static String RandomAccountNumber(int lenght)
        {
            char zeroChar = '0';
            char nineChar = '9';
            return String.Concat(Enumerable.Range(0, lenght).Select(_ => (char)CoreRandom.Next(zeroChar, nineChar + 1)));
        }

        public static Money RandomAmount(Decimal maxAmount)
        {
            return new Money() { Currency = Currencies.USD, Value = Decimal.Truncate(100 * maxAmount * (decimal)CoreRandom.NextDouble()) / 100 };
        }
    }

    public enum Currencies
    {
        USD = 1,
        CAN = 2
    }
}