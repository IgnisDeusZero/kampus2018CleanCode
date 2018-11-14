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
                .ToDigitsArray()
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

        private static int GetFactorSum(this long number)
        {
            return SumOfDigitsOnEvenPositions(number) * 3 + SumOfDigitsOnEvenPositions(number / 10);
        }

        private static int SumOfDigitsOnEvenPositions(this long number)
        {
            int sum = 0;
            if (number != 0)
            {
                var lastDigit = (int)(number % 10);
                sum = lastDigit + SumOfDigitsOnEvenPositions(number / 100);
            }
            return sum;
        }

        public static IEnumerable<int> ToDigitsArray(this long number)
        {
            return number
                .ToString()
                .Select(c => int.Parse(c.ToString()));
        }

        private static int GetWeightedSum(this IEnumerable<int> digitsArray)
        {
            int sum = 0;
            bool isOdd = true;
            foreach (int digit in digitsArray.Reverse())
            {
                sum += digit * (isOdd ? 3 : 1);
                isOdd = !isOdd;
            }

            return sum;
        }

        public static int GetFactorSum(this IEnumerable<int> digitsArray, IEnumerable<int> factorSource)
        {
            int sum = 0;
            var factor = factorSource.GetEnumerator();
            foreach (int digit in digitsArray.Reverse())
            {
                factor.MoveNext();
                sum += digit * factor.Current;
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
