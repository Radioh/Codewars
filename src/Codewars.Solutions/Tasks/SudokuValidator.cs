using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Sudoku Validator
    /// Write a function validSolution/ValidateSolution/valid_solution() 
    /// that accepts a 2D array representing a Sudoku board, and returns true if it is a valid solution, 
    /// or false otherwise. The cells of the sudoku board may also contain 0's, 
    /// which will represent empty cells. 
    /// Boards containing one or more zeroes are considered to be invalid solutions.
    ///https://www.codewars.com/kata/529bf0e9bdf7657179000008
    /// </summary>
    public class SudokuValidator : ITask
    {
        public string Name => "Sudoku Validator";
        public string Rank => "4 Kuy";

        public string Run()
        {
            var cases = new int[][][]
            {
                new int[][] {
                    new int[] { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
                    new int[] { 6, 7, 2, 1, 9, 5, 3, 4, 8 },
                    new int[] { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
                    new int[] { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
                    new int[] { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
                    new int[] { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
                    new int[] { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
                    new int[] { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
                    new int[] { 3, 4, 5, 2, 8, 6, 1, 7, 9 }
                },
                new int[][] {
                    new int[] { 5, 3, 4, 6, 7, 8, 9, 1, 2 }, 
                    new int[] { 6, 7, 2, 1, 9, 0, 3, 4, 8 },
                    new int[] { 1, 0, 0, 3, 4, 2, 5, 6, 0 },
                    new int[] { 8, 5, 9, 7, 6, 1, 0, 2, 0 },
                    new int[] { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
                    new int[] { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
                    new int[] { 9, 0, 1, 5, 3, 7, 2, 1, 4 },
                    new int[] { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
                    new int[] { 3, 0, 0, 4, 8, 1, 1, 7, 9 } 
                }
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = IsValid(testCase);
                results.Append($"IsValid(board) -> {result} \n");
            }

            return results.ToString();
        }

        private bool IsValid(int[][] board)
        {
            bool IsValid(int[] set) => !set.GroupBy(x => x)
                .Any(x => x.Count() > 1 || x.Key == 0);

            var cubes = new Dictionary<(int, int), List<int>>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                var col = new List<int>();
                for (int j = 0; j < board.Length; j++) 
                {
                    var value = board[i][j];
                    col.Add(value);
                    
                    var cubePos = (i/3, j/3);
                    if (cubes.ContainsKey(cubePos))
                        cubes[cubePos].Add(value);
                    else
                        cubes.Add(cubePos, new List<int>() { value });
                }

                if (!IsValid(col.ToArray()))
                    return false;
            }

            foreach (var row in board) 
                if (!IsValid(row)) 
                    return false;

            foreach (var cube in cubes.Select(x => x.Value)) 
                if (!IsValid(cube.ToArray())) 
                    return false;

            return true;
        }
    }
}
