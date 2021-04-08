using LogicalEngine;
using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{   
    /// <summary>
    /// Exhaust parts send exhaust units
    /// </summary>
    public abstract class ExhaustPart : CarPart
    {
        public override string UnitTypeSent { get => "Exhaust"; }

        protected ExhaustPart(Engine engine) : base(engine)
        {
        }
    }
}
