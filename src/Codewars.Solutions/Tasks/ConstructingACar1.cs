using System;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Constructing a car #1 - Engine and Fuel Tank
    /// You have to construct a car. Step by Step. Kata by Kata.
    /// First you have to implement the engine and the fuel tank.
    /// https://www.codewars.com/kata/578b4f9b7c77f535fc00002f
    /// </summary>
    public class ConstructingACar1 : ITask
    {
        public string Name => "Constructing a car #1 - Engine and Fuel Tank";
        public string Rank => "5 Kuy";

        public string Run()
        {
            return "Car methods tested";
        }
    }

    public interface ICar
    {
        bool EngineIsRunning { get; }
        void EngineStart();
        void EngineStop();
        void Refuel(double liters);
        void RunningIdle();
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

    public class Car : ICar
    {
        public IFuelTankDisplay fuelTankDisplay;
        public bool EngineIsRunning => _engine.IsRunning;

        private const double FuelConsumptionRate = 0.0003;
        private readonly IEngine _engine;
        private readonly IFuelTank _fuelTank;

        public Car()
        {
            _fuelTank = new FuelTank();
            _engine = new Engine(_fuelTank);
            fuelTankDisplay = new FuelTankDisplay(_fuelTank);
        }

        public Car(double fuelLevel)
        {
            _fuelTank = new FuelTank(fuelLevel);
            _engine = new Engine(_fuelTank);
            fuelTankDisplay = new FuelTankDisplay(_fuelTank);
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
            _engine.Consume(FuelConsumptionRate);
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
}
