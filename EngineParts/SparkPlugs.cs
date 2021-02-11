using LogicalCarEngine.Engines;
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
            var stroke = (Engine as CombustionEngine).StrokeCycler.StrokeCycle;
            return stroke == CombustionStrokeCycles.Combustion;
        }
        public override bool CanTransferTo(UnitContainer receiver)
        {
            var stroke = (Engine as CombustionEngine).StrokeCycler.StrokeCycle;

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