using System;
using System.Linq;
using NodaMoney;

namespace GettingStarted
{
    public static class RandomGenerator
    {
        private static readonly Random CoreRandom = new Random();

        public static String RandomAccountNumber(int lenght)
        {
            char zeroChar = '0';
            char nineChar = '9';
            return String.Concat(Enumerable.Range(0, lenght)
                .Select(_ => (char)CoreRandom.Next(zeroChar, nineChar + 1)));
        }

        public static Money RandomAmount(Decimal maxAmount)
        {
            return Money.USDollar(Decimal.Truncate(100 * maxAmount * (decimal)CoreRandom.NextDouble()) / 100);
        }
    }
}