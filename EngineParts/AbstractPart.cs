using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    /// <summary>
    /// Not a real car part, but acts like one.  Used to trigger other car parts
    /// </summary>
    public abstract class AbstractPart : CarPart
    {
        public override string UnitTypeSent { get => "N/A"; }

        public AbstractPart(Engine engine) : base(engine)
        {
            Engine = engine;
        }
    }
}
