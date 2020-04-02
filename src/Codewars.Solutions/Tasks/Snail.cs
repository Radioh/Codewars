using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Snail Sort
    /// Given an n x n array, return the array elements arranged from outermost elements to the middle element, traveling clockwise.
    /// </summary>
    public class Snail : ITask
    {
        public string Name => "Snail Sort";
        public string Rank => "4 Kuy";
        public string Link => "https://www.codewars.com/kata/521c2db8ddc89b9b7a0000c1";

        public string Run()
        {
            var cases = new int[][][]
            {
                new int[][]
                {
                    new int[]{ 1, 2, 3 },
                    new int[]{ 4, 5, 6 },
                    new int[]{ 7, 8, 9 },
                },
                new int[][]
                {
                    new int[]{ 1, 2, 3 },
                    new int[]{ 8, 9, 4 },
                    new int[]{ 7, 6, 5 },
                },
                new int[][]
                {
                    new int[]{ 1, 2, 3, 4 },
                    new int[]{ 12, 13, 14, 5 },
                    new int[]{ 11, 16, 15, 6 },
                    new int[]{ 10, 9, 8, 7 },
                },
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = SnailSort(testCase);
                var resultString = string.Join(" ", result);
                results.Append($"SnailSort(array) -> {resultString} \n");
            }

            return results.ToString();
        }

        private int[] SnailSort(int[][] input)
        {
            var totalLength = input.Select(x => x.Length).Sum();

            var lBound = 0;
            var rBound = input.GetLength(0) - 1;
            var topBound = 0;
            var btmBound = input.Length - 1;

            var visited = new List<int>();
            var x = -1;
            var y = 0;
            var dir = 'u';

            while (visited.Count() != totalLength)
            {
                if (dir == 'r' && x == rBound)
                {
                    dir = 'd';
                    topBound++;
                }
                else if (dir == 'd' && y == btmBound)
                {
                    dir = 'l';
                    rBound--;
                }
                else if (dir == 'l' && x == lBound)
                {
                    dir = 'u';
                    btmBound--;
                }
                else if (dir == 'u' && y == topBound)
                {
                    dir = 'r';

                    if (x != -1)
                        lBound++;
                } 

                if (dir == 'r') x++;
                else if (dir == 'l') x--;
                else if (dir == 'u') y--;
                else if (dir == 'd') y++;

                visited.Add(input[y][x]);
            }

            return visited.ToArray();
        }
    }
}
