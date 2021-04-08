using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineContainers
{
    public abstract class EngineSubsystem
    {
        public Engine Engine { get; }
        public List<CarPart> Parts { get; protected set; }
        public EngineSubsystem(Engine e)
        {
            Engine = e;
            Parts = new List<CarPart>();
        }
    }
}
