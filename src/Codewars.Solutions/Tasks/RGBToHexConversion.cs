using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// RGB To Hex Conversion
    /// https://www.codewars.com/kata/513e08acc600c94f01000001
    /// </summary>
    public class RgbToHexConversion : ITask
    {
        public string Name => "RGB To Hex Conversion";
        public string Rank => "5 Kuy";

        public string Run()
        {
            var cases = new int[][]
            {
                new int[] { 255, 255, 255},
                new int[] { 255, 255, 300},
                new int[] { 148, 0, 211},
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = Rgb(testCase[0], testCase[1], testCase[2]);
                results.Append($"Rgb() -> {result} \n");
            }

            return results.ToString();
        }

        private string Rgb(int r, int g, int b)
        {
            return $"{GetHexValue(Sanitize(r))}" +
                   $"{GetHexValue(Sanitize(g))}" +
                   $"{GetHexValue(Sanitize(b))}";

            int Sanitize(int val) 
            {
                if (val < 0) return 0;
                if (val > 255) return 255;
                return val;
            }

            string GetHexValue(int val) => val.ToString("X2");
        }
    }
}
