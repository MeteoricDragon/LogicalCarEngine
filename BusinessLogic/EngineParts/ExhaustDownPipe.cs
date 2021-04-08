using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.Cylinders;

namespace LogicalEngine.EngineParts
{
    public class ExhaustDownPipe : FuelPart
    {
        public override string UserFriendlyName { get => "Exhaust Down Pipe"; }


        public ExhaustDownPipe(Engine e) : base(e)
        {
            Engine = e;
        }
        protected override bool ShouldActivate(CarPart target)
        {
            return false;
        }

        public override bool TryTransferUnits(UnitContainer receiver)
        {
            EndOfSystemExpelUnits();
            return true;
        }

        private void EndOfSystemExpelUnits()
        {
            UnitsOwned = 0;
        }
    }
}