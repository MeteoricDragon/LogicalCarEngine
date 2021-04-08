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
            CanDrawFromFuelTank = true;
        }
    }
}