using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.Cylinders;

namespace LogicalEngine.EngineParts
{
    public class SparkPlugs : ElectricalPart
    {
        public override string UserFriendlyName { get => "Spark Plugs"; }
        public SparkPlugs(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override bool ShouldActivate(CarPart target, in bool transferSuccess)
        {
            var stroke = (target as Cylinders).StrokeCycle;
            return stroke == CombustionStrokeCycles.Combustion;
        }
        protected override bool CanTransfer(UnitContainer receiver)
        {
            var stroke = (receiver as Cylinders).StrokeCycle;

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