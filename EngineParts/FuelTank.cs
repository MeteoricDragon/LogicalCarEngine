using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class FuelTank : FuelPart
    {
        public override string UserFriendlyName { get => "Fuel Tank"; }
        public FuelTank(Engine e) : base(e)
        {
        }
    }
}