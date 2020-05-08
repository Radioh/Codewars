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
 \------+------S-------------+------/        /   |
        |                    |              /    |
        \--------------------+-------------/     |
                             |                   |
/-------------\              |                   |        
|             |              |             /-----+----\      
|             |              |             |     |     \    
\-------------+--------------+-----S-------+-----/      \   
              |              |             |             \
              |              |             |             |
              |              \-------------+-------------/
              |                            |
              \----------------------------/";

            var aTrain = "Aaaa";
            var aTrainPos = 147;
            var bTrain = "bbbbbbbbbbB";
            var bTrainPos = 288;
            var limit = 2000;

            var result = TrainCrash(track, aTrain, aTrainPos, bTrain, bTrainPos, limit);
            return $"TrainCrash() -> {result} \n";
        }

        public static int TrainCrash(string track, string aTrain,
         int aTrainPos, string bTrain, int bTrainPos, int limit)
        {
            var parsedTrack = ParseTrack(track, aTrainPos, bTrainPos);
            
            var turns = 0;
            var trainA = new Train(aTrain, parsedTrack.Length, aTrainPos);
            var trainB = new Train(bTrain, parsedTrack.Length, bTrainPos);
            //OverwriteStartingPos(parsedTrack, trainA, trainB);

            if (trainA.HasCollidedWith(trainB))
                return 0;

            while (turns != limit) 
            {
                MoveTurn(parsedTrack, trainA);
                MoveTurn(parsedTrack, trainB);
                turns++;

                if (trainA.HasCollidedWith(trainB))
                    return turns;
            }

            return -1;
        }

        private static string[] ParseTrack(string track, int aPos, int bPos) 
        {
            var trackArray = new List<string[]>();

            foreach (var line in track.Split(System.Environment.NewLine))
                trackArray.Add(line.Select(x => x.ToString()).ToArray());

            //var count = 0;
            var x = 0;
            var y = 0;
            var dir = Direction.Right;
            var traverse = true;
            var start = (-1, -1);
            var last = "";
            var traversedTrack = new List<string>();

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
                        else if (dir == Direction.Right)
                        {
                            dir = Direction.Down;
                            y++;
                        }
                        else if (dir == Direction.Left)
                        {
                            dir = Direction.Up;
                            y--;
                        }
                        else if (dir == Direction.Down || dir == Direction.DownRight)
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
                        else if (dir == Direction.Up || dir == Direction.UpLeft)
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
                        else if (dir == Direction.Right)
                        {
                            dir = Direction.Up;                            
                            y--;
                        }
                        else if (dir == Direction.Left)
                        {
                            dir = Direction.Down;
                            y++;
                        }
                        else if (dir == Direction.Down || dir == Direction.DownLeft)
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
                        else if (dir == Direction.Up || dir == Direction.UpRight)
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

                //if (count == aPos)
                //    current = "A" + current;

                //if (count == bPos)
                //    current = "B" + current;

                if (start == (-1, -1))
                    start = snap;

                //count++;
                last = current;
                traversedTrack.Add(current);

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

        //private static void OverwriteStartingPos(string[] parsedTrack, Train trainA, Train trainB) 
        //{
        //    for (int i = 0; i < parsedTrack.Length; i++)
        //    {
        //        if (parsedTrack[i].Length == 2) 
        //        {
        //            if (parsedTrack[i].Contains("A"))
        //                trainA.OverwritePosition(i);
        //            if (parsedTrack[i].Contains("B"))
        //                trainB.OverwritePosition(i);
                    
        //            parsedTrack[i] = parsedTrack[i].Substring(1);
        //        }
        //    }
        //}

        private static void MoveTurn(string[] parsedTrack, Train train) 
        {
            if (train.IsStoppedAtStation())
                return;
            
            train.Move();

            if (parsedTrack[train._position] == "S") 
                train.ReachStation();
        }
        
        public enum Direction
        {
            Up, Down, Left, Right, UpRight, UpLeft, DownRight, DownLeft
        }

        public class Train 
        {
            public string _layout;
            public bool _direction;
            public int _position;
            public int _stationTimeLeft;
            private bool _isExpress;
            private int _trackCount;

            public Train(string layout, int trackCount, int position) 
            {
                _layout = layout;
                _direction = !char.IsUpper(layout[0]);
                _trackCount = trackCount;
                _isExpress = layout.Contains("X");
                _position = position;
            }

            //public void OverwritePosition(int pos) 
            //{
            //    _position = pos;
            //}

            public void Move() 
            {
                if (_direction) 
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
                if (!_isExpress) 
                    _stationTimeLeft = _layout.Length;
            }

            public bool IsStoppedAtStation() 
            {
                var isStopped = _stationTimeLeft == 0;
                _stationTimeLeft--;

                return isStopped;
            }

            public int[] TakesUpPositions() 
            {
                var positions = new int[_layout.Length];
                var cur = _position;

                for (int i = 0; i < positions.Length; i++)
                {
                    positions[i] = cur;

                    if (_direction) 
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

            public bool HasCollidedWith(Train other) 
            {
                return TakesUpPositions().Intersect(other.TakesUpPositions()).Any();
            }
        }
    }
}
