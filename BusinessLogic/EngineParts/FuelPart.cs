using LogicalEngine;
using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{   
    /// <summary>
    /// Fuel parts send fuel units
    /// </summary>
    public abstract class FuelPart : CarPart
    {
        public override string UnitTypeSent { get => "Fuel"; }

        protected FuelPart(Engine engine) : base(engine)
        {
        }
    }
}
