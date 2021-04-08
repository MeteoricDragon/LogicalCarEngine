using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.Cylinders;

namespace LogicalEngine.EngineParts
{
    public class Distributor : ElectricalPart
    {
        public override string UserFriendlyName { get => "Distributor"; }

        public override int UnitTriggerThreshold { get => 5; }
        public Distributor(Engine e) : base(e)
        {
        }

        public override bool CanTransferTo(UnitContainer receivingPart)
        {
            var cycle = (Engine as CombustionEngine).StrokeCycler.StrokeCycle;
            if (cycle == CombustionStrokeCycles.Combustion)
                return true;
            return false;
        }
    }
}