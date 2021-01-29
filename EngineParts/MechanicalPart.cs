using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    /// <summary>
    /// Mechanical parts send physical units
    /// </summary>
    public abstract class MechanicalPart : CarPart
    {
        public int FrictionResistance { get; protected set; }
        public override string UnitTypeSent { get => "Momentum"; }
        protected MechanicalPart(Engine engine) : base(engine)
        {
        }
    }
}
