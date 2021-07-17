using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Make a spiral
    /// Your task, is to create a NxN spiral with a given size.
    /// https://www.codewars.com/kata/534e01fbbb17187c7e0000c6
    /// </summary>
    public class MakeASpiral : ITask
    {
        public string Name => "Make a spiral";
        public string Rank => "3 Kuy";

        public string Run()
        {
            /*
             00000
             ....0
             000.0
             0...0
             00000
            */

            var cases = new int[]
            {
                5, 10, 15,
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = Spiralize(testCase);
                results.Append($"Spiralize({testCase}) -> {WriteField(result)} \n");
            }

            return results.ToString();
        }

        private static int[,] Spiralize(int size)
        {
            var field = InitializeField(size);
            var dir = Direction.Right;
            (int Y, int X) pos = (0, 0);

            while (dir != Direction.None)
            {
                field[pos.Y, pos.X] = 1;

                pos = SetPosition(dir, pos);
                dir = SetDirection(field, dir, pos);
            }

            return field;
        }

        private static int[,] InitializeField(int size)
        {
            var field = new int[size, size];

            for (var i = 0; i < size; i++)
                for (var j = 0; j < size; j++)
                    field[i, j] = 0;

            return field;
        }

        private static (int, int) SetPosition(Direction dir, (int Y, int X) pos)
        {
            return dir switch
            {
                Direction.Up => (pos.Y - 1, pos.X),
                Direction.Down => (pos.Y + 1, pos.X),
                Direction.Left => (pos.Y, pos.X - 1),
                Direction.Right => (pos.Y, pos.X + 1),
                _ => throw new System.ArgumentException(),
            };
        }

        private static Direction SetDirection(int[,] field, Direction dir, (int Y, int X) pos)
        {
            var newDir = Direction.None;
            var boundary = field.GetLength(0) - 1;

            switch (dir)
            {
                case Direction.Up:
                    if (field[pos.Y - 2, pos.X] == 1 && field[pos.Y, pos.X + 2] == 0 && field[pos.Y + 1, pos.X + 1] == 0)
                        newDir = Direction.Right;
                    else if (field[pos.Y - 1, pos.X] == 0)
                        newDir = Direction.Up;
                    break;
                case Direction.Down:
                    if (pos.Y == boundary || (pos.Y + 2 <= boundary && field[pos.Y + 2, pos.X] == 1 && field[pos.Y, pos.X - 2] == 0 && field[pos.Y - 1, pos.X - 1] == 0))
                        newDir = Direction.Left;
                    else if (pos.Y < boundary && field[pos.Y + 1, pos.X] == 0)
                        newDir = Direction.Down;
                    break;
                case Direction.Left:
                    if (pos.X == 0 || (pos.X - 2 >= 0 && field[pos.Y, pos.X - 2] == 1 && field[pos.Y - 2, pos.X] == 0))
                        newDir = Direction.Up;
                    else if (pos.X > 0 && field[pos.Y, pos.X - 1] == 0)
                        newDir = Direction.Left;
                    break;
                case Direction.Right:
                    if (pos.X == boundary || (pos.X + 2 <= boundary && field[pos.Y, pos.X + 2] == 1 && field[pos.Y + 2, pos.X] == 0))
                        newDir = Direction.Down;
                    else if (pos.X < boundary && field[pos.Y, pos.X + 1] == 0)
                        newDir = Direction.Right;
                    break;
                default:
                    throw new System.ArgumentException();
            }

            return newDir;
        }

        private enum Direction
        {
            None,
            Up,
            Down,
            Left,
            Right
        }

        private string WriteField(int[,] field)
        {
            var resultstring = new StringBuilder();
            var boundary = field.GetLength(1);

            for (int i = 0; i < boundary; i++)
            {
                for (int j = 0; j < boundary; j++)
                {
                    resultstring.Append(field[i, j]);
                }

                resultstring.Append("\n");
            }

            System.Console.WriteLine(resultstring.ToString());
            return resultstring.ToString();
        }
    }
}
