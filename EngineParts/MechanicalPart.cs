using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public abstract class MechanicalPart : CarPart
    {
        public int FrictionResistance { get; protected set; }
        public override string UnitType { get => "Momentum"; }
        protected MechanicalPart(Engine engine) : base(engine)
        {
        }
    }
}
