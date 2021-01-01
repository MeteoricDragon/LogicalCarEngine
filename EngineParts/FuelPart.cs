using LogicalEngine;
using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public abstract class FuelPart : CarPart
    {
        public override string UnitType { get => "Fuel"; }

        protected FuelPart(Engine engine) : base(engine)
        {
        }
    }
}
