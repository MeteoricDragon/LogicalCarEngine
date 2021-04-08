using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class FuelTank : FuelPart
    {
        public override int UnitsMax { get => 100; }
        public override string UserFriendlyName { get => "Fuel Tank"; }
        public FuelTank(Engine e) : base(e)
        {
            Engine = e;
            UnitsOwned = UnitsMax;
        }
    }
}