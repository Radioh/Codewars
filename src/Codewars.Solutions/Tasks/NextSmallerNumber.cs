using System;
using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Next smaller number with the same digits
    /// Write a function that takes a positive integer and returns the next smaller positive integer containing the same digits.
    /// https://www.codewars.com/kata/5659c6d896bc135c4c00021e
    /// </summary>
    public class NextSmallerNumber : ITask
    {
        public string Name => "Next smaller number with the same digits";
        public string Rank => "4 Kuy";

        public string Run()
        {
            var cases = new long[]
            {
                21,
                907,
                531,
                9,
                441,
                123456798,
                111,
                1027,
                29009
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = NextSmallerNumberWithSameDigits(testCase);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private long NextSmallerNumberWithSameDigits(long input)
        { 
            var digits = input.ToString()
                .Select(x => Convert.ToInt64(x.ToString()))
                .ToList();

            var lft = -1;
            for (var i = digits.Count() - 1; i > 0; i--) 
                if (lft == -1 && digits[i] < digits[i - 1]) 
                    lft = i - 1;

            if (lft == -1)
                return -1;

            var rgt = lft + 1;
            var lftDigit = digits[lft];
            for (int i = lft + 1; i < digits.Count(); i++) 
                if (digits[rgt] < digits[i] && digits[i] < lftDigit)
                    rgt = i;

            var tmp = digits[lft];
            digits[lft] = digits[rgt];
            digits[rgt] = tmp;

            var untilSwap = digits.Take(lft + 1).ToList();
            var ordered = digits.Skip(lft + 1).OrderByDescending(x => x).ToList();
            untilSwap.AddRange(ordered);

            if (untilSwap[0] == 0)
                return -1;
            
            return long.Parse(string.Join("", untilSwap));
        }
    }
}
