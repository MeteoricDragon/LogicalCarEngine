using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineContainers
{
    public class PowerParts : EngineSubsystem
    {
        public PowerParts(Engine e) : base(e)
        {
            CarPart[] parts = { new Alternator(e), new Battery(e), new Distributor(e),
                                      new IgnitionCoil(e), new IgnitionSwitch(e),
                                      new SparkPlugs(e), new StarterMotor(e)};
            Parts.AddRange(parts);
        }
    }
}
