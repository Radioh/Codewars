using System;
using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Stop gninnipS My sdroW!
    /// Write a function that takes in a string of one or more words, and returns the same string, " +
    /// but with all five or more letter words reversed (Just like the name of this Kata). " +
    /// Strings passed in will consist of only letters and spaces. " +
    /// Spaces will be included only when more than one word is present.
    /// </summary>
    public class StopgninnipSMysdroW : ITask
    {
        public string Name => "Stop gninnipS My sdroW!";
        public string Rank => "6 Kuy";
        public string Link => "https://www.codewars.com/kata/5264d2b162488dc400000001";

        public string Run()
        {
            var cases = new string[]
            {
                    "Welcome",
                    "Hey fellow warriors",
                    "This is another test",
                    "You are almost to the last test",
                    "Just kidding there is still one more"
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = SpinWords(testCase);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private string SpinWords(string sentence)
        {
            return string.Join(" ", sentence.Split(" ").Select(x =>
                { return x.Count() >= 5 ? string.Join("", x.Reverse()) : x; }));
        }
    }
}
