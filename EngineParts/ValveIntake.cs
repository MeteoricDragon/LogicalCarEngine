using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;

namespace LogicalEngine.EngineParts
{
    public class ValveIntake : FuelPart, IValve
    {
        public override string UserFriendlyName { get => "Intake Valve"; }
        public bool IsOpen
        {
            get
            {
                return ((Engine as CombustionEngine).Chamber.StrokeCycle == CombustionStrokeCycles.Intake);
            }
        }
        public ValveIntake(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override bool CanFill(UnitContainer receiver)
        {
            return IsOpen;
        }

        protected override bool CanTransfer(UnitContainer receivingPart)
        {
            if (IsOpen)
                return base.CanTransfer(receivingPart);
            else
                return false;
        }

        protected override bool ShouldActivate(CarPart target)
        {
            return base.ShouldActivate(target);
        }
    }
}