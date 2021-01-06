using LogicalEngine.EngineContainers;
using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicalEngine.Engines
{
    /// <summary>
    /// Configurations for the engine part events.  This class should be a dependency?
    /// </summary>
    public class EngineOperationOrder
    {
        public Dictionary<CarPart, List<CarPart>> PartChain { get; protected set; }
        public EngineOperationOrder()
        { }
        private T FindPart<T>(List<CarPart> PartList) where T : CarPart
        {
            return PartList.Find(x => x is T) as T;
        }

        public void ConfigureICEOverheadValveEngine(ICEOverheadValveEngine engine)
        {
            var parts = engine.AllParts;

            var Accelerator = FindPart<Accelerator>(parts);
            var AirCleaner = FindPart<AirCleaner>(parts);
            var Alternator = FindPart<Alternator>(parts);
            var Battery = FindPart<Battery>(parts);
            var CamShaft = FindPart<CamShaft>(parts);
            var Carburetor = FindPart<Carburetor>(parts);
            var Crankshaft = FindPart<Crankshaft>(parts);
            var CombustionChamber = FindPart<CombustionChamber>(parts);
            var Distributor = FindPart<Distributor>(parts);
            var Flywheel = FindPart<Flywheel>(parts);
            var FuelPump = FindPart<FuelPump>(parts);
            var FuelTank = FindPart<FuelTank>(parts);
            var IgnitionCoil = FindPart<IgnitionCoil>(parts);
            var IgnitionSwitch = FindPart<IgnitionSwitch>(parts);
            var Pistons = FindPart<Pistons>(parts);
            var SparkPlugs = FindPart<SparkPlugs>(parts);
            var StarterMotor = FindPart<StarterMotor>(parts);
            var TimingChain = FindPart<TimingChain>(parts);
            var ValveExhaust = FindPart<ValveExhaust>(parts);
            var ValveIntake = FindPart<ValveIntake>(parts);

            PartChain = new Dictionary<CarPart, List<CarPart>>()
            {
                { Accelerator, new List<CarPart> { Carburetor } },
                { AirCleaner, new List<CarPart> { } },
                { Alternator, new List<CarPart> { Battery } },
                { Battery, new List<CarPart> { } },
                { CamShaft, new List<CarPart> { FuelPump, Distributor, ValveExhaust, ValveIntake } },
                { Carburetor, new List<CarPart> { ValveIntake } },
                { Crankshaft, new List<CarPart> { Alternator, TimingChain} },
                { CombustionChamber, new List<CarPart> { Pistons, ValveExhaust } },
                { Distributor, new List<CarPart> { SparkPlugs } },
                { Flywheel, new List<CarPart> { Crankshaft } },
                { FuelPump, new List<CarPart> { Carburetor } },
                { FuelTank, new List<CarPart> { } },
                { IgnitionCoil, new List<CarPart> { Distributor } },
                { IgnitionSwitch, new List<CarPart> { IgnitionCoil, StarterMotor } },
                { Pistons, new List<CarPart> { Crankshaft } },
                { SparkPlugs, new List<CarPart> { CombustionChamber } },
                { StarterMotor, new List<CarPart> { Flywheel } }, 
                { TimingChain, new List<CarPart> { CamShaft } },
                { ValveExhaust, new List<CarPart> { /* outside scope of program */ } },
                { ValveIntake, new List<CarPart> { CombustionChamber } } 
            };
        }

        public void ConnectBattery(Engine engine)
        {
            var allParts = engine.AllParts;
            var battery = FindPart<Battery>(allParts);
            foreach (CarPart p in allParts)
            {
                if (p.CanChargeBattery || p.CanDrawFromBattery)
                    p.Battery = battery;
            }
        }

        public List<CarPart> GetActivatingPartsOf(CarPart part)
        {
            return PartChain
                .Where(e => e.Value.Contains(part)) // Filter elements that contains the CarSystemPart within their list
                .Select(e => e.Key) // Select the key of each element
                .ToList(); // Transform the IEnumerable<> into a List<>
        }
    }
}
