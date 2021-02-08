using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineContainers
{
    public class ExhaustParts : EngineSubsystem
    {
        public ExhaustParts(Engine e) : base(e)
        {
            CarPart[] parts = { new ValveExhaust(e), new ExhaustDownPipe(e)};
            Parts.AddRange(parts);
        }
    }
}