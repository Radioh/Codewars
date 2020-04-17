using System;
using System.Linq;
using System.Numerics;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Last digit of a huge number
    /// For a given list [x1, x2, x3, ..., xn] compute the last (decimal) digit of x1 ^ (x2 ^ (x3 ^ (... ^ xn))). 
    /// https://www.codewars.com/kata/5518a860a73e708c0a000027
    /// </summary>
    public class LastDigitOfAhugeNumber : ITask
    {
        public string Name => "Last digit of a huge number";
        public string Rank => "3 Kuy";

        public string Run()
        {
            var cases = new int[][]
            {
                new int[] {82242, 254719, 736371}, // 8
                new int[] {2, 2, 2, 2}, // 6
                new int[] {}, // 1
                new int[] {0,0}, // 1
                new int[] {0,0,0}, // 0
                new int[] {1,2}, // 1
                new int[] {3,4,5}, //1
                new int[] {7,6,21}, // 1
                new int[] {12,30,21}, // 6
                new int[] {2,2,2,0}, // 4
                new int[] { 937640, 767456, 981242}, // 0,
                new int[] { 123232, 694022, 140249 }, // 6
                new int[] { 442776, 512027, 919527, 371360, 789266, 574245, 256725, 533278, 454455, 944055, 540776, 953948, 136523 }, // 6
                new int[] { 402893, 760765, 404965, 457528, 271441, 389652, 97925, 916804 }, // 3
                new int[] { 2147483647, 2147483647, 2147483647, 2147483647 } // 3
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = LastDigit(testCase);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private int LastDigit(int[] input)
        {
            return (int)input.Select(x => (long)x).Reverse().Aggregate(
                seed: (long)1,
                func: (acc, val) => {
                    var x = new BigInteger(val < 20 ? val : ((val % 20) + 20));
                    var y = Convert.ToInt32(acc < 4 ? acc : (acc % 4 + 4));
                    return (long)BigInteger.Pow(x, y);
                },
                resultSelector: result => result % 10);
        }
    }
}
