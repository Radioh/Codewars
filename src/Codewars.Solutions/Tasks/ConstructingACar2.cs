using System;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Constructing a car #2 - Driving
    /// You have to construct a car. Step by Step. Kata by Kata.
    /// As second step you have to implement the logic for driving, which includes accelerating, braking and free-wheeling.
    /// https://www.codewars.com/kata/578df8f3deaed98fcf0001e9
    /// </summary>
    public class ConstructingACar2 : ITask
    {
        public string Name => "Constructing a car #2 - Driving";
        public string Rank => "5 Kuy";

        public string Run()
        {
            return "Car methods tested";
        }

        public interface ICar
        {
            bool EngineIsRunning { get; }
            void EngineStart();
            void EngineStop();
            void Refuel(double liters);
            void RunningIdle();
            void BrakeBy(int speed); // car #2
            void Accelerate(int speed); // car #2
            void FreeWheel(); // car #2
        }

        public interface IEngine
        {
            bool IsRunning { get; }
            void Consume(double liters);
            void Start();
            void Stop();
        }

        public interface IFuelTank
        {
            double FillLevel { get; }
            bool IsOnReserve { get; }
            bool IsComplete { get; }
            void Consume(double liters);
            void Refuel(double liters);
        }

        public interface IFuelTankDisplay
        {
            double FillLevel { get; }
            bool IsOnReserve { get; }
            bool IsComplete { get; }
        }

        public interface IDrivingInformationDisplay // car #2
        {
            int ActualSpeed { get; }
        }

        public interface IDrivingProcessor // car #2
        {
            int ActualSpeed { get; }

            void IncreaseSpeedTo(int speed);

            void ReduceSpeed(int speed);
        }

        public class Car : ICar
        {
            public IFuelTankDisplay fuelTankDisplay;
            public IDrivingInformationDisplay drivingInformationDisplay; // car #2  
            public bool EngineIsRunning => _engine.IsRunning;

            private const double FuelConsumptionRate = 0.0003;
            private readonly IEngine _engine;
            private readonly IFuelTank _fuelTank;
            private readonly IDrivingProcessor _drivingProcessor; // car #2

            public Car()
            {
                _fuelTank = new FuelTank();
                _engine = new Engine(_fuelTank);
                _drivingProcessor = new DrivingProcessor();
                fuelTankDisplay = new FuelTankDisplay(_fuelTank);
                drivingInformationDisplay = new DrivingInformationDisplay(_drivingProcessor);
            }

            public Car(double fuelLevel)
            {
                _fuelTank = new FuelTank(fuelLevel);
                _engine = new Engine(_fuelTank);
                _drivingProcessor = new DrivingProcessor();
                fuelTankDisplay = new FuelTankDisplay(_fuelTank);
                drivingInformationDisplay = new DrivingInformationDisplay(_drivingProcessor);
            }

            public Car(double fuelLevel, int maxAcceleration) // car #2
            {
                _fuelTank = new FuelTank(fuelLevel);
                _engine = new Engine(_fuelTank);
                _drivingProcessor = new DrivingProcessor(maxAcceleration);
                fuelTankDisplay = new FuelTankDisplay(_fuelTank);
                drivingInformationDisplay = new DrivingInformationDisplay(_drivingProcessor);
            }

            public void EngineStart()
            {
                _engine.Start();
            }

            public void EngineStop()
            {
                _engine.Stop();
            }

            public void Refuel(double liters)
            {
                _fuelTank.Refuel(liters);
            }

            public void RunningIdle()
            {
                if (!_engine.IsRunning)
                    return;

                _engine.Consume(FuelConsumptionRate);
            }

            public void BrakeBy(int speed) // car #2
            {
                _drivingProcessor.ReduceSpeed(speed);

                var consumption = _drivingProcessor.ActualSpeed == 0
                    ? FuelConsumptionRate
                    : 0;

                _engine.Consume(consumption);
            }

            public void Accelerate(int speed) // car #2
            {
                if (!_engine.IsRunning)
                    return;

                if (speed < _drivingProcessor.ActualSpeed)
                    FreeWheel();
                else
                {
                    _drivingProcessor.IncreaseSpeedTo(speed);

                    _engine.Consume(GetConsumption());
                }
            }

            public void FreeWheel() // car #2
            {
                _drivingProcessor.ReduceSpeed(1);

                var consumption = _drivingProcessor.ActualSpeed == 0
                    ? FuelConsumptionRate
                    : 0;

                _engine.Consume(consumption);
            }

            private double GetConsumption()
            {
                var speed = _drivingProcessor.ActualSpeed;

                if (speed == 0)
                    return 0;
                if (speed < 61)
                    return 0.0020;
                if (speed < 101)
                    return 0.0014;
                if (speed < 141)
                    return 0.0020;
                if (speed < 201)
                    return 0.0025;

                return 0.0030;
            }
        }

        public class Engine : IEngine
        {
            public bool IsRunning { get; private set; }

            private readonly IFuelTank _fuelTank;

            public Engine(IFuelTank fuelTank)
            {
                _fuelTank = fuelTank;
            }

            public void Consume(double liters)
            {
                if (IsRunning)
                    _fuelTank.Consume(liters);

                if (_fuelTank.FillLevel == 0)
                    IsRunning = false;
            }

            public void Start()
            {
                if (_fuelTank.FillLevel > 0)
                    IsRunning = true;
            }

            public void Stop()
            {
                IsRunning = false;
            }
        }

        public class FuelTank : IFuelTank
        {
            public double FillLevel { get; private set; }
            public bool IsOnReserve => FillLevel < ReserveLimit;
            public bool IsComplete => FillLevel == MaxTankSize;

            private const int DefaultFuelLevel = 20;
            private const int MaxTankSize = 60;
            private const int ReserveLimit = 5;

            public FuelTank()
            {
                FillLevel = DefaultFuelLevel;
            }

            public FuelTank(double fillLevel)
            {
                if (fillLevel >= 0)
                    FillLevel = fillLevel;

                if (FillLevel > MaxTankSize)
                    FillLevel = MaxTankSize;
            }

            public void Consume(double liters)
            {
                if (liters < 0)
                    return;

                FillLevel -= liters;

                if (FillLevel < 0)
                    FillLevel = 0;
            }

            public void Refuel(double liters)
            {
                if (liters <= 0)
                    return;

                FillLevel += liters;

                if (FillLevel > MaxTankSize)
                    FillLevel = MaxTankSize;
            }
        }

        public class FuelTankDisplay : IFuelTankDisplay
        {
            public double FillLevel => Math.Round(_fuelTank.FillLevel, 2);
            public bool IsOnReserve => _fuelTank.IsOnReserve;
            public bool IsComplete => _fuelTank.IsComplete;

            private readonly IFuelTank _fuelTank;

            public FuelTankDisplay(IFuelTank fuelTank)
            {
                _fuelTank = fuelTank;
            }
        }

        public class DrivingInformationDisplay : IDrivingInformationDisplay // car #2
        {
            public int ActualSpeed => _drivingProcessor.ActualSpeed;

            private readonly IDrivingProcessor _drivingProcessor;

            public DrivingInformationDisplay(IDrivingProcessor drivingProcessor)
            {
                _drivingProcessor = drivingProcessor;
            }
        }

        public class DrivingProcessor : IDrivingProcessor // car #2
        {
            private const int DefaultAcceleration = 10;
            private const int DeAcceleration = 10;
            private const int MaxSpeed = 250;

            public int ActualSpeed { get; private set; }

            private int _acceleration = DefaultAcceleration;

            public DrivingProcessor()
            {

            }

            public DrivingProcessor(int acceleration)
            {
                if (acceleration < 5)
                    acceleration = 5;
                if (acceleration > 20)
                    acceleration = 20;

                _acceleration = acceleration;
            }

            public void IncreaseSpeedTo(int speed)
            {
                if (speed <= 0 || ActualSpeed >= speed)
                    return;

                ActualSpeed += _acceleration;

                ActualSpeed = Math.Min(ActualSpeed, speed);
                ActualSpeed = Math.Min(ActualSpeed, MaxSpeed);
            }

            public void ReduceSpeed(int speed)
            {
                if (speed < 0)
                    return;

                ActualSpeed -= Math.Min(DeAcceleration, speed);
                ActualSpeed = Math.Max(ActualSpeed, 0);
            }
        }
    }
}
