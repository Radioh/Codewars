using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Create Phone Number
    /// Write a function that accepts an array of 10 integers (between 0 and 9),
    /// that returns a string of those numbers in the form of a phone number.
    /// </summary>
    public class CreatePhoneNumber : ITask
    {
        public string Name => "Create Phone Number";
        public string Rank => "6 Kuy";
        public string Link => "https://www.codewars.com/kata/525f50e3b73515a6db000b83";

        public string Run()
        {
            var cases = new int[][]
            {
                new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 },
                new int[]{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = CreateNumber(testCase);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private string CreateNumber(int[] numbers)
        {
            var first = string.Join("", numbers.Take(3));
            var second = string.Join("", numbers.Skip(3).Take(3));
            var third = string.Join("", numbers.Skip(6).Take(4));

            return $"({first}) {second}-{third}";
        }
    }
}
