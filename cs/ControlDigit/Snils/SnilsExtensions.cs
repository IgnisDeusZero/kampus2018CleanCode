using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlDigit
{
    
    public static class SnilsExtensions
    {
        public static int CalculateSnils(this long number)
        {
            return number
                .ToDigitsArray()
                .Reverse()
                .GetFactorSum(GetSequence())
                .GetControlDigit();
        }

        private static IEnumerable<int> GetSequence()
        {
            int i = 1;
            while (true)
            {
                yield return i;
                i++;
            }
        }
        private static int GetControlDigit(this int sum)
        {
            if (sum < 100)
            {
                return sum;
            }

            else if (sum > 101)
            {
                return (sum % 101).GetControlDigit();
            }

            return 0;
        }
    }
}
