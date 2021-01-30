using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;

namespace LogicalEngine.EngineParts
{
    public class SparkPlugs : ElectricalPart
    {
        public override string UserFriendlyName { get => "Spark Plugs"; }
        public SparkPlugs(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override bool CanTransfer(UnitContainer transferingPart)
        {
            var Chamber = (Engine as CombustionEngine).Chamber;
            if (Chamber.StrokeCycle == CombustionStrokeCycles.Combustion)
                return true;
            return false;
        }
    }
}