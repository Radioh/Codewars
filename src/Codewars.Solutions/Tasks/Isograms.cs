using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Isograms
    /// An isogram is a word that has no repeating letters, consecutive or non-consecutive. 
    /// Implement a function that determines whether a string that contains only letters is an isogram. " +
    /// Assume the empty string is an isogram. Ignore letter case.
    /// https://www.codewars.com/kata/54ba84be607a92aa900000f1
    /// </summary>
    public class Isograms : ITask
    {
        public string Name => "Isograms";
        public string Rank => "7 Kuy";

        public string Run()
        {
            var cases = new string[]
            {
                "Dermatoglyphics",
                "aba",
                "moOse"
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = IsIsoGram(testCase);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private bool IsIsoGram(string input)
        {
            return !input.ToLower()
                .GroupBy(x => x)
                .Any(x => x.Count() > 1);
        }
    }
}
