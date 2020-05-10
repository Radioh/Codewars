using System.Collections.Generic;
using System.Linq;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Blaine is pain
    /// https://www.codewars.com/kata/59b47ff18bcb77a4d1000076
    /// </summary>
    public class BlaineIsPain : ITask
    {
        public string Name => "Blaine is pain";
        public string Rank => "2 Kuy";

        public string Run()
        {
            var track = @"                                
                                            /------------\             
            /-------------\                /             |             
            |             |               /              S             
            |             |              /               |             
            |        /----+--------------+------\        |
            \       /     |              |      |        |             
             \      |     \              |      |        |             
             |      |      \-------------+------+--------+---\         
             |      |                    |      |        |   |         
             \------+--------------------+------/        /   |         
                    |                    |              /    |         
                    \------S-------------+-------------/     |         
                                         |                   |         
            /-------------\              |                   |         
            |             |              |             /-----+----\    
            |             |              |             |     |     \   
            \-------------+--------------+-----S-------+-----/      \  
                          |              |             |             \ 
                          |              |             |             | 
                          |              \-------------+-------------/ 
                          |                            |               
                          \----------------------------/               ";

            var a = "Aaaa";
            var aPos = 147;
            var b = "Bbbbbbbbbbb";
            var bPos = 288;
            var limit = 1000;

            //var track = @" 
            ///-------\ 
            //|       | 
            //|       | 
            //|       | 
            //\-------+--------\
            //        |        |
            //        S        |
            //        |        |
            //        \--------/";

            //var a = "aaaA";
            //var aPos = 22;
            //var b = "bbbbB";
            //var bPos = 0;
            //var limit = 100;

            var result = TrainCrash(track, a, aPos, b, bPos, limit);
            return $"TrainCrash() -> {result} \n";
        }

        public static int TrainCrash(string track, string aTrain,
         int aTrainPos, string bTrain, int bTrainPos, int limit)
        {
            System.Console.WriteLine(track);
            System.Console.WriteLine(aTrain);
            System.Console.WriteLine(aTrainPos);
            System.Console.WriteLine(bTrain);
            System.Console.WriteLine(bTrainPos);
            System.Console.WriteLine(limit);

            var parsedTrack = ParseTrack(track);
            var trainA = new Train(aTrain, parsedTrack.Length, aTrainPos);
            var trainB = new Train(bTrain, parsedTrack.Length, bTrainPos);
            var turns = 0;

            if (trainA.HasCrashed(trainB, parsedTrack))
                return turns;

            while (turns != limit)
            {
                turns++;

                trainA.Move(parsedTrack[trainA._position].IsStation);
                trainB.Move(parsedTrack[trainB._position].IsStation);

                if (trainA.HasCrashed(trainB, parsedTrack))
                    return turns;
            }

            return -1;
        }

        private static (int Position, bool IsStation)[] ParseTrack(string track) 
        {
            var trackArray = new List<string[]>();

            foreach (var line in track.Split(System.Environment.NewLine))
                trackArray.Add(line.Select(l => l.ToString()).ToArray());

            var x = 0;
            var y = 0;
            var dir = Direction.Right;
            var traverse = true;
            var start = (-1, -1);
            var count = 0;

            var trackPositions = new Dictionary<(int, int), int>();
            var traversedTrack = new List<(int, bool)>();

            while(traverse)
            {
                var current = trackArray[y][x];
                var snap = (y, x);
                
                switch (current)
                {
                    case "-":
                        if (dir == Direction.Left)
                            x--;
                        else
                            x++;
                        break;

                    case "|":
                        if (dir == Direction.Down)
                            y++;
                        else if (dir == Direction.Up)
                            y--;
                        break;

                    case "\\":
                        if (start == (-1, -1))
                        {
                            dir = Direction.Down;
                            y++;
                        }
                        else if (dir == Direction.Down || dir == Direction.DownRight || dir == Direction.Right)
                        {
                            if (InBounds(y + 1, x + 1, trackArray) && 
                                (trackArray[y + 1][x + 1].Contains("\\") ||
                                 trackArray[y + 1][x + 1].Contains("S") ||
                                 trackArray[y + 1][x + 1].Contains("X")))
                            {
                                dir = Direction.DownRight;
                                x++;
                                y++;
                            }
                            else if (InBounds(y + 1, x, trackArray) && 
                                (trackArray[y + 1][x].Contains("|") ||
                                 trackArray[y + 1][x].Contains("+") ||
                                 trackArray[y + 1][x].Contains("S")))
                            {
                                dir = Direction.Down;
                                y++;
                            }
                            else
                            {
                                dir = Direction.Right;
                                x++;
                            }
                        }
                        else if (dir == Direction.Up || dir == Direction.UpLeft || dir == Direction.Left)
                        {
                            if (InBounds(y - 1, x - 1, trackArray) && 
                                (trackArray[y - 1][x - 1].Contains("\\") ||
                                 trackArray[y - 1][x - 1].Contains("S") ||
                                 trackArray[y - 1][x - 1].Contains("X")))
                            {
                                dir = Direction.UpLeft;
                                x--;
                                y--;
                            }
                            else if (InBounds(y - 1, x, trackArray) &&
                               (trackArray[y - 1][x].Contains("|") ||
                                trackArray[y - 1][x].Contains("S") ||
                                trackArray[y - 1][x].Contains("+")))
                            {
                                dir = Direction.Up;
                                y--;
                            }
                            else
                            {
                                dir = Direction.Left;
                                x--;
                            }
                        }
                        break;

                    case "/":
                        if (start == (-1, -1))
                        {
                            dir = Direction.Right;
                            x++;
                        }
                        else if (dir == Direction.Down || dir == Direction.DownLeft || dir == Direction.Left)
                        {
                            if (InBounds(y + 1, x - 1, trackArray) &&
                                (trackArray[y + 1][x - 1].Contains("/") ||
                                 trackArray[y + 1][x - 1].Contains("X") ||
                                 trackArray[y + 1][x - 1].Contains("S")))
                            {
                                dir = Direction.DownLeft;
                                x--;
                                y++;
                            }
                            else if(InBounds(y + 1, x, trackArray) && 
                                (trackArray[y + 1][x].Contains("|") ||
                                 trackArray[y + 1][x].Contains("+") ||
                                 trackArray[y + 1][x].Contains("S")))
                            {
                                dir = Direction.Down;
                                y++;
                            }
                            else
                            {
                                dir = Direction.Left;
                                x--;
                            }
                        }
                        else if (dir == Direction.Up || dir == Direction.UpRight || dir == Direction.Right)
                        {
                            if (InBounds(y - 1, x + 1, trackArray) && 
                                (trackArray[y - 1][x + 1].Contains("/") ||
                                 trackArray[y - 1][x + 1].Contains("X") ||
                                 trackArray[y - 1][x + 1].Contains("S")))
                            {
                                dir = Direction.UpRight;
                                x++;
                                y--;
                            }
                            else if (InBounds(y - 1, x, trackArray) && 
                                (trackArray[y - 1][x].Contains("|") ||
                                 trackArray[y - 1][x].Contains("+") ||
                                 trackArray[y - 1][x].Contains("S")))
                            {
                                dir = Direction.Up;
                                y--;
                            }
                            else
                            {
                                dir = Direction.Right;
                                x++;
                            }
                        }
                        break;

                    case "+":
                        if (dir == Direction.Down)
                            y++;
                        else if (dir == Direction.Up)
                            y--;
                        else if (dir == Direction.Left)
                            x--;
                        else if (dir == Direction.Right)
                            x++;
                        break;

                    case "X":
                        if (dir == Direction.DownLeft)
                        {
                            x--;
                            y++;
                        }
                        else if (dir == Direction.DownRight)
                        {
                            x++;
                            y++;
                        }
                        else if (dir == Direction.UpLeft)
                        {
                            x--;
                            y--;
                        }
                        else if (dir == Direction.UpRight)
                        {
                            x++;
                            y--;
                        }
                        break;

                    case "S":
                        if (dir == Direction.Down)
                            y++;
                        else if (dir == Direction.Up)
                            y--;
                        else if (dir == Direction.Left)
                            x--;
                        else if (dir == Direction.Right)
                            x++;
                        if (dir == Direction.DownLeft)
                        {
                            x--;
                            y++;
                        }
                        else if (dir == Direction.DownRight)
                        {
                            x++;
                            y++;
                        }
                        else if (dir == Direction.UpLeft)
                        {
                            x--;
                            y--;
                        }
                        else if (dir == Direction.UpRight)
                        {
                            x++;
                            y--;
                        }
                        break;
                    default:
                        if (trackArray[y].Length - 1 == x)
                        {
                            x = 0;
                            y++;
                        }
                        else
                            x++;
                        continue;
                }

                if (start == (-1, -1))
                    start = snap;

                var isStation = current.Contains("S");
               
                if (trackPositions.TryGetValue(snap, out var value))
                    traversedTrack.Add((value, isStation));
                else
                {
                    trackPositions.Add(snap, count);
                    traversedTrack.Add((count, isStation));
                    count++;
                }

                if (start == (y, x))
                    traverse = false;
            }

            return traversedTrack.ToArray();
        }

        private static bool InBounds(int y, int x, List<string[]> track)
        {
            if (track.Count() <= y || y < 0 || track[y].Length <= x || x < 0)
                return false;

            return true;
        }
        
        public enum Direction
        {
            Up, Down, Left, Right, UpRight, UpLeft, DownRight, DownLeft
        }

        public class Train 
        {
            public string _layout;
            public bool _clockwise;
            public int _position;
            public int _stationTimeLeft;
            private bool _isExpress;
            private int _trackCount;
            private bool _departFromStation;

            public Train(string layout, int trackCount, int position) 
            {
                _layout = layout;
                _clockwise = !char.IsUpper(layout[0]);
                _trackCount = trackCount;
                _isExpress = layout.Contains("X");
                _position = position;
                _departFromStation = true;
            }

            public void Move(bool station) 
            {
                if (!_departFromStation)
                {
                    if (station)
                        ReachStation();

                    if (IsStoppedAtStation())
                        return;
                }

                _departFromStation = false;

                if (_clockwise) 
                {
                    _position++;            

                    if (_position == _trackCount)
                        _position = 0;
                }
                else 
                {
                    _position--;

                    if (_position == -1)
                        _position = _trackCount - 1;
                }

            }

            public void ReachStation() 
            {
                if (!_isExpress && _stationTimeLeft == 0) 
                    _stationTimeLeft = _layout.Length;
            }

            private bool IsStoppedAtStation() 
            {
                if (_stationTimeLeft == 0)
                    return false;

                return --_stationTimeLeft > 0;
            }

            public int[] TakesUpPositions((int Position, bool IsStation)[] parsedTrack) 
            {
                var positions = new int[_layout.Length];
                var cur = _position;

                for (int i = 0; i < positions.Length; i++)
                {
                    positions[i] = parsedTrack[cur].Position;

                    if (_clockwise) 
                    {
                        cur--;

                        if (cur == -1) 
                            cur = _trackCount - 1;
                    }
                    else 
                    {
                        cur++;

                        if (cur == _trackCount)
                            cur = 0;
                    }
                }

                return positions;
            }

            public bool HasCrashed(Train other, (int Position, bool IsStation)[] parsedTrack) 
            {
                var positions = TakesUpPositions(parsedTrack);
                var otherPositions = other.TakesUpPositions(parsedTrack);

                if (positions.GroupBy(x => x).Any(x => x.Count() > 1))
                    return true;

                if (otherPositions.GroupBy(x => x).Any(x => x.Count() > 1))
                    return true;

                if (positions.Intersect(otherPositions).Any())
                    return true;

                return positions.Intersect(otherPositions).Any();
            }
        }
    }
}
