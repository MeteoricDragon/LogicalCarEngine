using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class Crankshaft : MechanicalPart
    {
        public override string UserFriendlyName { get => "Crankshaft"; }
        public override int UnitsToGive { get => 15; }
        public override int UnitsMax { get => 50; }
        public Crankshaft(Engine e) : base(e)
        {
            UnitsOwned = 5;
            FrictionResistance = 0;
        }
    }
}