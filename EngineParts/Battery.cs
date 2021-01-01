using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class Battery : ElectricalPart
    {
        public override string UserFriendlyName { get => "Battery"; }
        public override int UnitsMax { get => 100; }
        public Battery(Engine e) : base(e)
        {
            UnitsOwned = UnitsMax;
        }
    }
}