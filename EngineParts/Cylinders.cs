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
        public override int UnitsToGive => 40;

        // TODO: consume all fuel when changing from Combustion to exhaust and after transfer to pistons.
        public override string UserFriendlyName { get => "Cylinders"; }
        public Cylinders(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override bool ShouldActivate(CarPart target, in bool transferSuccess)
        {
            var threshold = IsAtUnitThreshold(target);
            
            return (target is ValveExhaust exhaust && exhaust.IsOpen && threshold) 
                || (target is Pistons && threshold);

        }
        protected override bool CanTransfer(UnitContainer receiver)
        {
            if ((receiver is ValveExhaust exhaust && exhaust.IsOpen ) 
                || (receiver is Pistons))
                return true;
            return false;
        }
        protected override bool CanFill(UnitContainer receiver)
        {
            if ((receiver is ValveExhaust exhaust && exhaust.IsOpen)
                || (receiver is Pistons))
                return true;
            return false;
        }
    }
}
