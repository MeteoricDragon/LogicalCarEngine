using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class FuelPump : FuelPart
    {
        public override string UserFriendlyName { get => "Fuel Pump"; }
        public FuelPump(Engine e) : base(e)
        {
            CanDrawFuel = true;
        }

        protected override bool ShouldActivate(CarPart activatingPart)
        {
            return (activatingPart is CamShaft
                || base.ShouldActivate(activatingPart)); ;
        }

        protected override bool CanFill(UnitContainer givingPart)
        {
            if (givingPart is CamShaft)
                return false;
            return true;
        }
    }
}