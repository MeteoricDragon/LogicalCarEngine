using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class SparkPlugs : ElectricalPart
    {
        public override int UnitsToGive { get => 0; } // nothing gets added to the fuel
        public override string UserFriendlyName { get => "Spark Plugs"; }
        public SparkPlugs(Engine e) : base(e)
        {          
        }

        // todo: prevent this from changing cycle to compression
    }
}