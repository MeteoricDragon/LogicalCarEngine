using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public abstract class ElectricalPart : CarPart
    {
        public override string UnitType { get => "Electricity"; }

        protected ElectricalPart(Engine engine) : base(engine)
        {
        }
    }
}
