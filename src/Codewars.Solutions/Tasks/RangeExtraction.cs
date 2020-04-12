using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Range Extraction
    /// https://www.codewars.com/kata/51ba717bb08c1cd60f00002f
    /// </summary>
    public class RangeExtraction : ITask
    {
        public string Name => "Range Extraction";
        public string Rank => "4 Kuy";

        public string Run()
        {
            var cases = new int[][] 
            {
                new int[] { -6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20 }
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = GetRanges(testCase);
                results.Append($"{string.Join(", ", testCase)} -> {result} \n");
            }

            return results.ToString();
        }

        private string GetRanges(int[] input)
        {
            var builder = new StringBuilder();
            var current = input.First();
            var final = input.First();

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] != final + 1)
                {
                    builder.Append(Format(current, final));
                    current = input[i];
                }

                final = input[i];
            }

            builder.Append(Format(current, final));

            string Format(int start, int end)
            {
                if (start == end) return $"{start},";
                if (end - start < 2) return $"{start},{end},";
                return $"{start}-{end},";
            }

            return builder.Remove(builder.Length - 1, 1).ToString();
        }
    }
}
