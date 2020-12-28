using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class Alternator : ElectricalPart
    {
        public override string UserFriendlyName { get => "Alternator"; }
        public Alternator(Engine e) : base(e)
        {
            CanChargeBattery = true;
        }
    }
}