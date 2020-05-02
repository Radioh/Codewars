using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Happy Birthday, Darling!
    /// As you may know, once some people pass their teens, they jokingly only celebrate their 20th or 21st birthday, forever.
    /// With some maths skills, that's totally possible - you only need to select the correct number base!
    /// https://www.codewars.com/kata/5e96332d18ac870032eb735f
    /// </summary>
    public class HappyBirthdayDarling : ITask
    {
        public string Name => "HappyBirthdayDarling";
        public string Rank => "7 Kuy";

        public string Run()
        {
            var cases = new int[] { 32, 39 };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = WomensAge(testCase);
                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private string WomensAge(int n)
        {
            var b = 10;
            string a;

            while(true)
            {
                a = ConvertToBase(n, b);
                if (a == "20" || a == "21") 
                    break;

                b++;
            }

            return $"{n}? That's just {a}, in base {b}!";
        }

        private static string ConvertToBase(int value, int toBase)
        {
            const string baseDigits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var result = string.Empty;

            while (value > 0)
            {
                result = baseDigits[value % toBase] + result;
                value /= toBase;
            }

            return result;
        }
    }
}
