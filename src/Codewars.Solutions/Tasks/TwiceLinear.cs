using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Twice Linear
    /// Given parameter n the function dbl_linear (or dblLinear...)
    /// returns the element u(n) of the ordered (with <) sequence u (so, there are no duplicates).
    /// https://www.codewars.com/kata/5672682212c8ecf83e000050
    /// </summary>
    public class TwiceLinear : ITask
    {
        public string Name => "Twice Linear";
        public string Rank => "4 Kuy";

        public string Run()
        {
            var cases = new int[]
            {
                10, 20, 30, 50, 100, 30000
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = DoubleLinear(testCase);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private int DoubleLinear(int n)
        {
            int CalcY(int x) => (2 * x) + 1;
            int CalcZ(int x) => (3 * x) + 1;

            var sortedSet = new SortedSet<int>() { 1 };

            for (int i = 0; i < n; i++)
            {
                var min = sortedSet.First();
                
                sortedSet.Remove(min);
                sortedSet.Add(CalcY(min));
                sortedSet.Add(CalcZ(min));
            }

            return sortedSet.First();
        }
    }
}
