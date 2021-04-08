using LogicalEngine.Engines;
using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineContainers
{
    public class AbstractParts : EngineSubsystem
    {
        public AbstractParts(Engine e) : base(e)
        {
            CarPart[] parts = { new CombustionStrokeCycler(e)};
            Parts.AddRange(parts);
        }
    }
}