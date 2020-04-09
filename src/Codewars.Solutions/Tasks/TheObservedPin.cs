using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// The Observed Pin
    /// Alright, detective, one of our colleagues successfully observed our target person, Robby the robber. 
    /// We followed him to a secret warehouse, where we assume to find all the stolen stuff.
    /// The door to this warehouse is secured by an electronic combination lock.
    /// Unfortunately our spy isn't sure about the PIN he saw, when Robby entered it.
    /// https://www.codewars.com/kata/5263c6999e0f40dee200059d
    /// </summary>
    public class TheObservedPin : ITask
    {
        public string Name => "The Observed Pin";
        public string Rank => "4 Kuy";

        public string Run()
        {
            var cases = new string[]
            {
                "11"
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = GeneratePins(testCase);
                results.Append($"{testCase} -> {string.Join(", ", result)} \n");
            }

            return results.ToString();
        }

        private List<string> GeneratePins(string observed)
        {
            var pins = new Dictionary<string, string[]>()
            {
                { "1", new []{ "1", "2", "4" }},
                { "2", new []{ "2", "1", "3", "5" }},
                { "3", new []{ "3", "2", "6" }},
                { "4", new []{ "4", "1", "5", "7" }},
                { "5", new []{ "5", "2", "4", "6", "8" }},
                { "6", new []{ "6", "3", "5", "9" }},
                { "7", new []{ "7", "4", "8" }},
                { "8", new []{ "8", "5", "7", "9", "0" }},
                { "9", new []{ "9", "6", "8" }},
                { "0", new []{ "0", "8" }},
            };

            var lists = new List<List<string>>();
            var digits = observed.Select(x => x.ToString());

            foreach (var digit in digits)
                lists.Add(pins[digit].ToList());

            var combinations = CartesianProduct(lists);

            return combinations.Select(x => string.Join(string.Empty, x)).ToList();

            // https://ericlippert.com/2010/06/28/computing-a-cartesian-product-with-linq/
            IEnumerable<IEnumerable<string>> CartesianProduct(IEnumerable<IEnumerable<string>> sequences)
            {
                IEnumerable<IEnumerable<string>> emptyProduct = new[] { Enumerable.Empty<string>() };

                return sequences.Aggregate(emptyProduct, (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] { item }));
            }
        }
    }
}
