using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class FuelTank : CarPart
    {
        public override string UserFriendlyName { get => "Fuel Tank"; }
        public FuelTank(Engine e) : base(e)
        {
        }
    }
}