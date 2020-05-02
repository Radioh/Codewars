using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Total amount of points
    /// https://www.codewars.com/kata/5bb904724c47249b10000131
    /// </summary>
    public class TotalAmountOfPoints : ITask
    {
        public string Name => "Total amount of points";
        public string Rank => "8 Kuy";

        public string Run()
        {
            var cases = new string[][]
            {
                new string[] { "1:0", "2:0", "3:0", "4:0", "2:1", "3:1", "4:1", "3:2", "4:2", "4:3" }
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = TotalPoints(testCase);
                results.Append($"TotalPoints() -> {result} \n");
            }

            return results.ToString();
        }

        private int TotalPoints(string[] games)
        {
            var result = 0;
            foreach (var game in games)
            {
                var split = game.Split(":");
                var x = int.Parse(split.First());
                var y = int.Parse(split.Last());

                if (x > y) result += 3;
                if (x == y) result += 1;
            }

            return result;
        }
    }
}
