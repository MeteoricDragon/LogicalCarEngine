using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    /// <summary>
    /// Electrical parts send electrical units
    /// </summary>
    public abstract class ElectricalPart : CarPart
    {
        public override string UnitTypeSent { get => "Electricity"; }

        protected ElectricalPart(Engine engine) : base(engine)
        {
            Engine = engine;
        }
    }
}
