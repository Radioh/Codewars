using System;
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
            var track = "";
            var aTrain = "Aaaa";
            var aTrainPos = 147;
            var bTrain = "Bbbbbbbbbbb";
            var bTrainPos = 288;
            var limit = 1000;

            var result = TrainCrash(track, aTrain, aTrainPos, bTrain, bTrainPos, limit);
            return $"TrainCrash() -> {result} \n";
        }

        public static int TrainCrash(string track, string aTrain,
         int aTrainPos, string bTrain, int bTrainPos, int limit)
        {
            var parsedTrack = ParseTrack(track, aTrainPos, bTrainPos);
            
            var turns = 0;
            var trainA = new Train(aTrain, parsedTrack.Length);
            var trainB = new Train(bTrain, parsedTrack.Length);
            OverwriteStartingPos(parsedTrack, trainA, trainB);
            
            while(turns != limit) 
            {
                MoveTurn(parsedTrack, trainA);
                MoveTurn(parsedTrack, trainB);

                if (trainA.HasCollidedWith(trainB))
                    break;

                turns++;
            }

            if (turns == limit)
                return -1;

            if (turns == 0)
                return 0;

            return turns;
        }

        private static string[] ParseTrack(string track, int aPos, int bPos) 
        {
            return Array.Empty<string>();

            // traverse og indsæt "A" sammen med skinne type på position
            // samme med b 
        }

        private static void OverwriteStartingPos(string[] parsedTrack, Train trainA, Train trainB) 
        {
            for (int i = 0; i < parsedTrack.Length; i++)
            {
                if (parsedTrack[i].Length == 2) 
                {
                    if (parsedTrack[i].Contains("A"))
                        trainA.OverwritePosition(i);
                    if (parsedTrack[i].Contains("B"))
                        trainB.OverwritePosition(i);
                    
                    parsedTrack[i] = parsedTrack[i].Substring(1);
                }
            }
        }

        private static void MoveTurn(string[] parsedTrack, Train train) 
        {
            if (train.IsStoppedAtStation())
                return;
            
            train.Move();

            if (parsedTrack[train._position] == "S") 
                train.ReachStation();
        }
        
        public class Train 
        {
            public string _layout;
            public bool _direction;
            public int _position;
            public int _stationTimeLeft;
            private bool _isExpress;
            private int _trackCount;

            public Train(string layout, int trackCount) 
            {
                _layout = layout;
                _direction = !char.IsUpper(layout[0]);
                _trackCount = trackCount;
            }

            public void OverwritePosition(int pos) 
            {
                _position = pos;
            }

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
