using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Prime Streaming (PG-13)
    /// https://www.codewars.com/kata/5519a584a73e70fa570005f5/
    /// </summary>
    public class PrimeStreamingPg13 : ITask
    {
        public string Name => "Prime Streaming (PG-13)";
        public string Rank => "3 Kuy";

        public string Run()
        {
            var results = new StringBuilder();

            var result = Stream();
            results.Append($"Stream() -> {string.Join(" ", result.Take(1_000_000))} \n");

            return results.ToString();
        }

        // Sieve of Eratosthenes
        private IEnumerable<int> Stream()
        {
            var max = 20_000_000;
            var numbers = new bool[max + 1];

            for (int i = 2; i <= max; i++)
                numbers[i] = true;

            for (int i = 2; i <= max; i++)
            {
                if (numbers[i])
                {
                    yield return i;
                    for (int j = i * 2; j <= max; j += i)
                        numbers[j] = false;
                }
            }
        }
    }
}
