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

        protected override bool ShouldActivate(CarPart target)
        {
            var stroke = (target as CombustionChambers).StrokeCycle;
            return stroke == CombustionStrokeCycles.Combustion;
        }
        protected override bool CanTransfer(UnitContainer receiver)
        {
            var stroke = (receiver as CombustionChambers).StrokeCycle;

            if (stroke == CombustionStrokeCycles.Combustion)
                return true;
            return false;
        }
        protected override bool CanFill(UnitContainer receiver)
        {
            return false;
        }
    }
}