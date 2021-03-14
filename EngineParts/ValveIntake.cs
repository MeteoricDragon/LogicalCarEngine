using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.Cylinders;

namespace LogicalEngine.EngineParts
{
    public class ValveIntake : FuelPart, IValve
    {
        public override string UserFriendlyName { get => "Intake Valve"; }
        public bool IsOpen { get; set; }
        public ValveIntake(Engine e) : base(e)
        {
            Engine = e;
        }

        public override bool CanBeDrainedBy(UnitContainer receiver)
        {
            return IsOpen;
        }

        public override bool CanTransferTo(UnitContainer receivingPart)
        {
            if (IsOpen)
                return base.CanTransferTo(receivingPart);
            else
                return false;
        }
    }
}