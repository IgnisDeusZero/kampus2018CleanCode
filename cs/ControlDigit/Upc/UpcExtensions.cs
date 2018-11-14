using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace ControlDigit
{
    public static class UpcExtensions
    {
        public static int CalculateUpc(this long number)
        {
            return number
                .GetDigitsFromLowToHigh()
                .GetFactorSum(GetFactors())
                .GetControlDigit();
        }

        private static IEnumerable<int> GetFactors()
        {
            int i = 3;
            while (true)
            {
                yield return i;
                i = 4 - i;
            }
        }

        public static IEnumerable<int> GetDigitsFromLowToHigh(this long number)
        {
            do
            {
                yield return (int)(number % 10);
                number /= 10;
            } while (number > 0);
        }

        public static int GetFactorSum(this IEnumerable<int> digitsArray, IEnumerable<int> factorSource)
        {
            int sum = 0;
            using (var factor = factorSource.GetEnumerator())
            {
                foreach (int digit in digitsArray)
                {
                    if (!factor.MoveNext())
                    {
                        throw new ArgumentException(nameof(factorSource));
                    }
                    sum += digit * factor.Current;
                }
            }
            
            return sum;
        }

        private static int GetControlDigit(this int sum)
        {
            var lastDigit = sum % 10;
            int res = 0;
            if (lastDigit != 0)
            {
                res = 10 - lastDigit;
            }

            return res;
        }
    }
}
