using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlDigit
{
    public static class SnilsExtensions
    {
        public static int CalculateSnils(this long number)
        {
            var digits = GetReversedDigits(number);
            var prod = SequencesProduct(digits, Enumerable.Range(1, digits.Count));

            prod %= 101;
            return prod == 100 ? 0 : prod;
        }

        private static List<int> GetReversedDigits(long number)
        {
            var result = new List<int>();
            while (number > 0)
            {
                result.Add((int)(number % 10));
                number /= 10;
            }

            return result;
        }

        private static int SequencesProduct(IEnumerable<int> first, IEnumerable<int> second)
        {
            return first
                .Zip(second, (a, b) => a * b)
                .Sum();
        }
    }
}
