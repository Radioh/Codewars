using System;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Human readable duration format
    /// Your task in order to complete this Kata is to write a function which formats a duration,
    /// given as a number of seconds, in a human-friendly way.
    /// The function must accept a non-negative integer.If it is zero, it just returns "now".
    /// Otherwise, the duration is expressed as a combination of years, days, hours, minutes and seconds.
    /// </summary>
    public class HumanReadableDurationFormat : ITask
    {
        public string Name => "Human Readable Duration Format";
        public string Rank => "4 Kuy";
        public string Link => "https://www.codewars.com/kata/52742f58faf5485cae000b9a";

        public string Run()
        {
            var cases = new int[]
            {
                    0,
                    1,
                    62,
                    120,
                    3662,
                    15731080,
                    132030240,
                    205851834,
                    253374061,
                    242062374,
                    101956166,
                    33243586
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = Format(testCase);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private string Format(int input)
        {
            if (input <= 0)
                return "now";

            string Plural(int x) => x <= 1 ? string.Empty : "s";
            string Format(int x, string unit) => x != 0 ? x.ToString() + $" {unit}{Plural(x)}, " : string.Empty;

            var time = TimeSpan.FromSeconds(input);
            var yearCount = time.Days / 365;

            if (yearCount > 0)
                time = time.Add(-TimeSpan.FromDays(yearCount * 365));

            var readable = ($"{Format(yearCount, "year")}" +
                           $"{Format(time.Days, "day")}" +
                           $"{Format(time.Hours, "hour")}" +
                           $"{Format(time.Minutes, "minute")}" +
                           $"{Format(time.Seconds, "second")}").Trim();

            readable = readable.Remove(readable.Length - 1);
            var lastComma = readable.LastIndexOf(",");

            if (lastComma != -1)
                readable = readable.Remove(lastComma, 1).Insert(lastComma, " and");

            return readable;
        }
    }
}
