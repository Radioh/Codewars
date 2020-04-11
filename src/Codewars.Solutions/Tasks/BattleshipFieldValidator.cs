using System.Collections.Generic;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Battleship Field Validator
    /// https://www.codewars.com/kata/52bb6539a4cf1b12d90005b7
    /// </summary>
    public class BattleshipFieldValidator : ITask
    {
        public string Name => "Battleship Field Validator";
        public string Rank => "3 Kuy";

        public string Run()
        {
            var testCase = new int[,] {
                {0, 0, 1, 0, 0, 1, 1, 0, 0, 0},     
                {0, 0, 1, 0, 0, 0, 0, 0, 1, 0},     
                {0, 0, 0, 0, 1, 1, 1, 0, 1, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                {0, 0, 0, 1, 1, 1, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 1, 0, 0, 1, 0, 0}
            };

            var result = ValidateBattlefield(testCase);

            return $"ValidateBattlefield(testcase) -> {result}";
        }

        private bool ValidateBattlefield(int[,] field)
        {
            var ships = new Dictionary<int, int>() {{1, 0}, {2, 0}, {3, 0}, {4, 0}};
            var visited = new HashSet<(int, int)>();            
            
            for (int y = 0; y < 10; y++)
                for (int x = 0; x < 10; x++)           
                    if (field[y, x] == 1 && !visited.Contains((y, x))) 
                        if (!ValidateShip(y, x))
                            return false;
            
            return ValidateAllShips();

            bool ValidateShip(int y, int x) 
            {
                bool? dir = null;
                var size = 1;

                if (x + 1 < 10 && field[y, x + 1] == 1)
                    dir = true;
                
                if (y + 1 < 10 && field[y + 1, x] == 1)
                {
                    if (dir.HasValue)
                        return false;

                    dir = false;
                }
                
                while(dir.HasValue) 
                {
                    visited.Add((y, x));
                    
                    if (y - 1 > -1 && dir.Value)
                        if (field[y - 1, x] == 1)
                            return false;
                    
                    if (y + 1 < 10 && dir.Value) 
                        if (field[y + 1, x] == 1)
                            return false;

                    if (x + 1 < 10 && !dir.Value)
                        if (field[y, x + 1] == 1)
                            return false;
                    
                    if (x - 1 > -1 && !dir.Value)
                        if (field[y, x - 1] == 1)
                            return false;

                    if (x - 1 > -1 && y - 1 > -1) 
                        if (field[y - 1, x - 1] == 1)
                            return false;

                    if (x + 1 < 10 && y - 1 > -1) 
                        if (field[y - 1, x + 1] == 1)
                            return false;

                    if (x - 1 > -1 && y + 1 < 10) 
                        if (field[y + 1, x - 1] == 1)
                            return false;

                    if (x + 1 < 10 && y + 1 < 10) 
                        if (field[y + 1, x + 1] == 1)
                            return false;

                    if (dir.Value) x++;
                    else y++;

                    if (x == 10 || y == 10 || field[y, x] == 0)                      
                        break;

                    size++;
                }

                if (size > 4) 
                    return false;
                
                ships[size]++;
                return true;
            }

            bool ValidateAllShips() 
            {   
                if (ships.Count != 4)
                    return false;

                var currentCount = 4;
                for (int i = 1; i <= 4; i++)
                {
                    var hasSize = ships.TryGetValue(i, out var count);
                    if (!hasSize || count != currentCount)
                        return false;

                    currentCount--;
                }

                return true;
            };
        }
    }
}
