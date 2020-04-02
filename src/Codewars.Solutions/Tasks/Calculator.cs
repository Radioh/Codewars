using System.Data;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Calculator
    /// Create a simple calculator that given a string of operators (), +, -, *, / 
    /// and numbers separated by spaces returns the value of that expression
    /// </summary>
    public class Calculator : ITask
    {
        public string Name => "Calculator";
        public string Rank => "3 Kuy";
        public string Link => "https://www.codewars.com/kata/5235c913397cbf2508000048";

        public string Run()
        {
            var cases = new string[]
            {
                "2 / 2 + 3 * 4 - 6"
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = Calculate(testCase);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private double Calculate(string input)
        {
            return double.Parse(new DataTable().Compute(input, null).ToString());
        }
    }
}
