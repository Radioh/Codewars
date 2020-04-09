using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Strip Comments
    /// Complete the solution so that it strips all text that follows any of a set of comment markers passed in.
    /// Any whitespace at the end of the line should also be stripped out. 
    /// https://www.codewars.com/kata/51c8e37cee245da6b40000bd
    /// </summary>
    public class StripComments : ITask
    {
        public string Name => "Strip Comments";
        public string Rank => "4 Kuy";

        public string Run()
        {
            var cases = new string[]
            {
                "apples, pears # and bananas\ngrapes\nbananas !apples",
            };

            var commentSymbols = new string[] { "#", "!" };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = Strip(testCase, commentSymbols);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private string Strip(string text, string[] commentSymbols)
        {
            var symbols = commentSymbols.Select(char.Parse).ToArray();
            var lines = text.Split("\n");

            for (int i = 0; i < lines.Count(); i++)
            {
                var index = lines[i].IndexOfAny(symbols);

                if (index != -1)
                    lines[i] = lines[i].Substring(0, index);

                lines[i] = lines[i].TrimEnd();
            }

            return string.Join("\n", lines);
        }
    }
}
