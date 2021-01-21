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
        public override int UnitsToGive { get => 0; } // nothing gets added to the fuel
        public override string UserFriendlyName { get => "Spark Plugs"; }
        public SparkPlugs(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override bool TransferConditionsMet(CarPart transferingPart)
        {
            var Chamber = (Engine as CombustionEngine).Chamber;
            if (Chamber.StrokeCycle == CombustionStrokeCycles.Combustion)
                return true;
            return false;
        }
    }
}