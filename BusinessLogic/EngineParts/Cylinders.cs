using LogicalEngine;
using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class Cylinders : FuelPart
    {
        public override int UnitsToGive => 10;

        // TODO: consume all fuel when changing from Combustion to exhaust and after transfer to pistons.
        public override string UserFriendlyName { get => "Cylinders"; }
        public Cylinders(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override bool ShouldActivate(CarPart target)
        {
            var baseConditions = base.ShouldActivate(target);
            var openExhaustValve = (target is ValveExhaust exhaust && exhaust.IsOpen);
            
            return ((openExhaustValve || target is Pistons) && baseConditions);

        }
        public override bool CanTransferTo(UnitContainer receiver)
        {
            if ((receiver is ValveExhaust exhaust && exhaust.IsOpen ) 
                || (receiver is Pistons))
                return true;
            return false;
        }

    }
}
