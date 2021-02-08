using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.Cylinders;

namespace LogicalEngine.EngineParts
{
    public class ValveExhaust : ExhaustPart, IValve
    {
        public override string UserFriendlyName { get => "Exhaust Valve"; }
        public bool IsOpen { 
            get {
                return ((Engine as CombustionEngine).Chamber.StrokeCycle == CombustionStrokeCycles.Exhaust);
            } 
        }

        public ValveExhaust(Engine e) : base(e)
        {
            Engine = e;
        }
        protected override bool CanTransfer(UnitContainer target)
        {
            if (IsOpen)
            {
                return base.CanTransfer(target);
            }
            return false;
        }
        protected override bool ShouldActivate(CarPart target, in bool transferSuccess, in bool didAdjustment)
        {
            if (target is Cylinders && IsOpen && !IsAtUnitThreshold(target)
                || target is ExhaustDownPipe)
                return true;
            return false;
        }

        public override bool TryTransferUnits(UnitContainer receiver)
        {
            if (receiver is Cylinders)
                return false;
            return base.TryTransferUnits(receiver);
        }
    }
}