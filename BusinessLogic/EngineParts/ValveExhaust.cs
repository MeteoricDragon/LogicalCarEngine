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
        public bool IsOpen { get; set; }

        public ValveExhaust(Engine e) : base(e)
        {
            Engine = e;
        }
        public override bool CanTransferTo(UnitContainer target)
        {
            if (IsOpen)
            {
                return base.CanTransferTo(target);
            }
            return false;
        }
        protected override bool ShouldActivate(CarPart target)
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

        protected override void RefreshEngineStage(CarPart target)
        {
        }
    }
}