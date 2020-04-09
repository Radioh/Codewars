using System;
using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Next bigger number with the same digits
    /// You have to create a function that takes a positive integer number
    /// and returns the next bigger number formed by the same digits
    /// https://www.codewars.com/kata/55983863da40caa2c900004e
    /// </summary>
    public class NextBiggerNumber : ITask
    {
        public string Name => "Next bigger number with the same digits";
        public string Rank => "4 Kuy";

        public string Run()
        {
            var cases = new long[]
            {
                12,
                111,
                513,
                2017,
                5843,
                1234,
                218765,
                534976
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = NextBiggerNumberWithSameDigits(testCase);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private long NextBiggerNumberWithSameDigits(long input)
        { 
            var digits = input.ToString()
                .Select(x => Convert.ToInt64(x.ToString()))
                .ToList();

            var lft = -1;
            for (var i = digits.Count() - 1; i > 0; i--) 
                if (lft == -1 && digits[i] > digits[i - 1]) 
                    lft = i - 1;

            if (lft == -1)
                return -1;

            var rgt = lft + 1;
            var lftDigit = digits[lft];
            for (int i = lft + 1; i < digits.Count(); i++) 
                if (digits[rgt] > digits[i] && digits[i] > lftDigit)
                    rgt = i;

            var tmp = digits[lft];
            digits[lft] = digits[rgt];
            digits[rgt] = tmp;

            var untilSwap = digits.Take(lft + 1).ToList();
            var ordered = digits.Skip(lft + 1).OrderBy(x => x).ToList();
            untilSwap.AddRange(ordered);
            
            return long.Parse(string.Join("", untilSwap));
        }
    }
}
