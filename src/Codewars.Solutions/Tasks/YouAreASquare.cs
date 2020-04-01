using System;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// A square of squares
    /// You like building blocks.You especially like building blocks that are squares.And what you even like more,
    /// is to arrange them into a square of square building blocks!
    /// However, sometimes, you can't arrange them into a square. 
    /// Instead, you end up with an ordinary rectangle! Those blasted things! If you just had a way to know, 
    /// whether you're currently working in vain… Wait! That's it! You just have to check if your number of building blocks is a perfect square.
    /// </summary>
    public class YouAreASquare : ITask
    {
        public string Name => "You're a square!";
        public string Rank => "7 Kuy";
        public string Link => "https://www.codewars.com/kata/54c27a33fb7da0db0100040e";

        public string Run()
        {
            var cases = new int[]
            {
                -1, 0, 3, 4, 25, 26
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = IsSquare(testCase);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private bool IsSquare(int n)
        {
            return Math.Sqrt(n) % 1 == 0;
        }
    }
}
