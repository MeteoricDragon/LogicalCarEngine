using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineContainers
{
    public class FuelParts : EngineSubsystem
    {
        public FuelParts(Engine e) : base(e)
        {
            CarPart[] parts = { new Accelerator(e), new AirCleaner(e), new Carburetor(e),
                                      new FuelPump(e), new FuelTank(e)};
            Parts.AddRange(parts);
        }
    }
}
