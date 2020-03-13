using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlDigit
{
    public static class UpcExtensions
    {
        public static int CalculateUpc(this long number)
        {
            var digits = GetDigits(number);
            var preControlNumber = GetMagicSum(digits) % 10;
            return preControlNumber == 0 ? 0 : 10 - preControlNumber;
        }

        private static int GetMagicSum(IEnumerable<int> digits)
        {
            return digits
                    .Select((d, i) => i % 2 == 0 ? d * 3 : d)
                    .Sum();
        }

        private static List<int> GetDigits(long number)
        {
            var result = new List<int>();
            while (number > 0)
            {
                result.Add((int) (number % 10));
                number /= 10;
            }

            return result;
        }


    }
}
