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
            var CombustionParts = engine.Subsystems.Find(x => x is CombustionParts).Parts;
            var FuelParts = engine.Subsystems.Find(x => x is FuelParts).Parts;
            var PowerParts = engine.Subsystems.Find(x => x is PowerParts).Parts;

            var Accelerator = FindPart<Accelerator>(FuelParts);
            var AirCleaner = FindPart<AirCleaner>(FuelParts);
            var Alternator = FindPart<Alternator>(PowerParts);
            var Battery = FindPart<Battery>(PowerParts);
            var CamShaft = FindPart<CamShaft>(CombustionParts);
            var Carburetor = FindPart<Carburetor>(FuelParts);
            var Crankshaft = FindPart<Crankshaft>(CombustionParts);
            var CombustionChamber = FindPart<CombustionChamber>(CombustionParts);
            var Distributor = FindPart<Distributor>(PowerParts);
            var Flywheel = FindPart<Flywheel>(CombustionParts);
            var FuelPump = FindPart<FuelPump>(FuelParts);
            var FuelTank = FindPart<FuelTank>(FuelParts);
            var IgnitionCoil = FindPart<IgnitionCoil>(PowerParts);
            var IgnitionSwitch = FindPart<IgnitionSwitch>(PowerParts);
            var Pistons = FindPart<Pistons>(CombustionParts);
            var SparkPlugs = FindPart<SparkPlugs>(PowerParts);
            var StarterMotor = FindPart<StarterMotor>(PowerParts);
            var TimingChain = FindPart<TimingChain>(CombustionParts);
            var ValveExhaust = FindPart<ValveExhaust>(CombustionParts);
            var ValveIntake = FindPart<ValveIntake>(CombustionParts);

            PartChain = new Dictionary<CarPart, List<CarPart>>()
            {
                { Accelerator, new List<CarPart> { Carburetor } },
                { AirCleaner, new List<CarPart> { } },
                { Alternator, new List<CarPart> { Battery } },
                { Battery, new List<CarPart> {  } },
                { CamShaft, new List<CarPart> { FuelPump, Distributor, ValveExhaust, ValveIntake } },
                { Carburetor, new List<CarPart> { ValveIntake } },
                { Crankshaft, new List<CarPart> { Alternator, TimingChain, Pistons} },
                { CombustionChamber, new List<CarPart> { Pistons } },
                { Distributor, new List<CarPart> { SparkPlugs } },
                { Flywheel, new List<CarPart> { Crankshaft } },
                { FuelPump, new List<CarPart> { Carburetor } },
                { FuelTank, new List<CarPart> {  } },
                { IgnitionCoil, new List<CarPart> { Distributor } },
                { IgnitionSwitch, new List<CarPart> { IgnitionCoil, StarterMotor } },
                { Pistons, new List<CarPart> { Crankshaft } },
                { SparkPlugs, new List<CarPart> { CombustionChamber } },
                { StarterMotor, new List<CarPart> { Flywheel } }, // disconnect after startup 
                { TimingChain, new List<CarPart> { CamShaft } },
                { ValveExhaust, new List<CarPart> {  } },
                { ValveIntake, new List<CarPart> { CombustionChamber } } 
            };
        }

        public void ConnectBattery(Engine engine)
        {
            var PowerParts = engine.Subsystems.Find(x => x is PowerParts).Parts;
            var battery = FindPart<Battery>(PowerParts);
            foreach (CarPart p in PowerParts)
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
