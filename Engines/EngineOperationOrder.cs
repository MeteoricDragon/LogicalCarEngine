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

            var accelerator = FindPart<Accelerator>(parts);
            var airCleaner = FindPart<AirCleaner>(parts);
            var alternator = FindPart<Alternator>(parts);
            var battery = FindPart<Battery>(parts);
            var camShaft = FindPart<CamShaft>(parts);
            var carburetor = FindPart<Carburetor>(parts);
            var crankshaft = FindPart<Crankshaft>(parts);
            var cylinders = FindPart<Cylinders>(parts);
            var distributor = FindPart<Distributor>(parts);
            var flywheel = FindPart<Flywheel>(parts);
            var fuelPump = FindPart<FuelPump>(parts);
            var fuelTank = FindPart<FuelTank>(parts);
            var ignitionCoil = FindPart<IgnitionCoil>(parts);
            var ignitionSwitch = FindPart<IgnitionSwitch>(parts);
            var pistons = FindPart<Pistons>(parts);
            var sparkPlugs = FindPart<SparkPlugs>(parts);
            var starterMotor = FindPart<StarterMotor>(parts);
            var timingChain = FindPart<TimingChain>(parts);
            var valveExhaust = FindPart<ValveExhaust>(parts);
            var valveIntake = FindPart<ValveIntake>(parts);
            var exhaustDown = FindPart<ExhaustDownPipe>(parts);

            PartChain = new Dictionary<CarPart, List<CarPart>>()
            {
                { accelerator, new List<CarPart> { carburetor } },
                { airCleaner, new List<CarPart> { } },
                { alternator, new List<CarPart> { battery } },
                { battery, new List<CarPart> { } },
                { camShaft, new List<CarPart> { fuelPump, valveIntake, distributor, valveExhaust} },
                { carburetor, new List<CarPart> { valveIntake } },
                { crankshaft, new List<CarPart> { alternator, timingChain} },
                { cylinders, new List<CarPart> { valveExhaust, pistons} },
                { distributor, new List<CarPart> { sparkPlugs } },
                { flywheel, new List<CarPart> { crankshaft } },
                { fuelPump, new List<CarPart> { carburetor } },
                { fuelTank, new List<CarPart> { } },
                { ignitionCoil, new List<CarPart> { distributor } },
                { ignitionSwitch, new List<CarPart> { ignitionCoil, starterMotor } },
                { pistons, new List<CarPart> { crankshaft } },
                { sparkPlugs, new List<CarPart> { cylinders } },
                { starterMotor, new List<CarPart> { flywheel } }, 
                { timingChain, new List<CarPart> { camShaft } },
                { valveExhaust, new List<CarPart> { cylinders, exhaustDown } },
                { valveIntake, new List<CarPart> { cylinders } } 
            };
        }

        public void ConnectBackup(Engine engine)
        {
            var allParts = engine.AllParts;
            var battery = FindPart<Battery>(allParts);
            var fuelTank = FindPart<FuelTank>(allParts);
            foreach (CarPart p in allParts)
            {

                if (p.CanChargeBattery || p.CanDrawFromBattery)
                    p.Reservoir = battery;
                else if (p.CanDrawFuel)
                    p.Reservoir = fuelTank;

                var activatingParts = GetActivatingPartsOf(p);
                foreach (CarPart aP in activatingParts)
                {
                    if (p.HasBackupSource && aP.IsBackupSource)
                        p.BackupSources.Add(aP);   
                    // TODO: but what if the backup source has different units than what is needed?
                    // Like Distributor getting Momentum from camshaft, when needing electricity from Ignition Coil?

                
                }
                
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
