using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineContainers
{
    public class CombustionParts : EngineSubsystem
    {
        public CombustionParts(Engine e) : base(e)
        {
            CarPart[] parts = { new Crankshaft(e), new Flywheel(e), new Pistons(e), new Cylinders(e),
                                        new ValveIntake(e), new ValveExhaust(e), new TimingChain(e), new CamShaft(e)};
            Parts.AddRange(parts);
        }
    }
}