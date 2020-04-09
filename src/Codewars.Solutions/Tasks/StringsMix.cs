using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Strings Mix
    /// Given two strings s1 and s2, we want to visualize how different the two strings are. We will only take into account the lowercase letters (a to z).
    /// https://www.codewars.com/kata/5629db57620258aa9d000014
    /// </summary>
    public class StringsMix : ITask
    {
        public string Name => "Strings Mix";
        public string Rank => "4 Kuy";

        public string Run()
        {
            var cases = new string[]
            {
                "A aaaa bb c",
                "my&friend&Paul has heavy hats! &",
            };

            var cases2 = new string[] 
            {
                "& aaa bbb c d",
                "my friend John has many many friends &"
            };

            var results = new StringBuilder();

            for (int i = 0; i < cases.Length; i++)
            {
                var result = Mix(cases[i], cases2[i]);
                results.Append($"{cases[i]} | {cases2[i]} -> {result} \n");   
            }

            return results.ToString();
        }

        private string Mix(string s1, string s2)
        {
            var dicS1 = s1.GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .ToDictionary(k => k.Key, v => v.Count());
            
            var dicS2 = s2.GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .ToDictionary(k => k.Key, v => v.Count());

            var unique = (s1 + s2).Replace(" ", "").Where(char.IsLower).ToHashSet();
            var combined = new List<(char c, char pfx, int cnt)>();

            foreach(var c in unique) 
            {
                var hasS1 = dicS1.TryGetValue(c, out var cntS1);
                var hasS2 = dicS2.TryGetValue(c, out var cntS2);

                if (!hasS1 && !hasS2)
                    continue;

                var cnt = Math.Max(cntS1, cntS2);
                var pfx = cntS1 > cntS2 ? '1' : '2';

                if (cntS1 == cntS2) 
                    pfx = '=';

                combined.Add((c, pfx, cnt));
            }

            var ordered = combined.OrderByDescending(x => x.cnt)
                .ThenBy(x => x.pfx)
                .ThenBy(x => x.c);

            var formats = ordered.Select(x => $"{x.pfx}:{new string(x.c, x.cnt)}");

            return string.Join("/", formats);
        }
    }
}
